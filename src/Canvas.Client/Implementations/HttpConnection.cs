using Canvas.Client.Entities;
using Canvas.Client.Interfaces;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Logging;

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Canvas.Client.Implementations;

/// <summary>
/// An <see cref="ICanvasConnection"/> that uses HTTP to connect to a Canvas instance.
/// </summary>
public sealed class HttpConnection
    : ICanvasConnection
{
    // Some constant header values to use with any requests
    private const string HeaderJsonMediaType = "application/json+canvas-string-ids";
    private const string HeaderRateLimitRemaining = "x-rate-limit-remaining";
    private const string HeaderRequestCost = "x-request-cost";
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpConnection> _logger;

    /// <summary>
    /// Initialise a new <see cref="HttpConnection"/> instance.
    /// </summary>
    /// <param name="apiUrl">The URL to the Canvas instance.</param>
    /// <param name="apiKey">The API key.</param>
    /// <param name="httpClient">The underlying <see cref="HttpClient"/> instance.</param>
    /// <param name="logger">The logger to use.</param>
    public HttpConnection(
        string apiUrl,
        string apiKey,
        HttpClient httpClient,
        ILogger<HttpConnection> logger)
    {
        // Validate the inputs
        Guard.IsNotNullOrWhiteSpace(apiUrl);
        Guard.IsNotNullOrWhiteSpace(apiKey);

        // Store the parameters for later use
        _httpClient = httpClient;
        _logger = logger;

        // Initialise the HTTP client
        _httpClient.BaseAddress = new Uri(apiUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HeaderJsonMediaType));
        _httpClient.DefaultRequestHeaders.UserAgent.Clear();

        // Add a user agent (now required by the Canvas API)
        var version = typeof(HttpConnection).Assembly?.GetName()?.Version?.ToString(2) ?? "2.0";
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Canvas.Net.Client", version));
    }

    /// <summary>
    /// The threshold at which to start rate limiting.
    /// </summary>
    /// <remarks>
    /// The threshold will be checked against the ratio of remaining limit / cost. If the cost is low and the limit is high, then
    /// we don't need to throttle. If the other way around, then we need to start slowing down the rate of requests. We do this by
    /// adding a delay after each expensive request.
    /// </remarks>
    public double ThrottlingThreshold { get; set; } = 10.0;

    /// <summary>
    /// Performs a low-level GET operation.
    /// </summary>
    /// <param name="url">The URL to call with the GET operation.</param>
    /// <param name="throwExceptionOnFailure">Whether to throw an exception if the server returns a non-success code.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>An <see cref="HttpResponseMessage"/> instance containing the response from the server.</returns>
    public async Task<HttpResponseMessage> Get(
        string url,
        bool throwExceptionOnFailure = true,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Sending {operation} to {url}", "GET", url);
        var response = await _httpClient
            .GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        _logger.LogDebug("Received {status} from {operation}:{url} - GET", response.StatusCode, "GET", url);
        await CheckResponse(
                url,
                response,
                throwExceptionOnFailure ? [] : [HttpStatusCode.NotFound],
                cancellationToken)
            .ConfigureAwait(false);
        await ApplyThrottling(response, cancellationToken)
            .ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Perform a GET operation and deserialise the response.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to deserialise.</typeparam>
    /// <param name="url">The URL to GET.</param>
    /// <param name="options">The options to pass to the URL.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The deserialised entity if valid; <see langword="null"/> otherwise.</returns>
    public async Task<TEntity?> GetEntity<TEntity>(
            string url,
            ICanvasOptions options,
            CancellationToken cancellationToken)
        where TEntity : class
    {
        var fullUrl = url;
        _logger.LogDebug("Getting {type} from {url}", typeof(TEntity).Name, fullUrl);

        // Retrieve the response from the server
        var response = await Get(fullUrl, throwExceptionOnFailure: false, cancellationToken)
            .ConfigureAwait(false);
        if (!response.IsSuccessStatusCode) return null;

        // Deserialise the response into the entity type
        var item = await response.Content.ReadFromJsonAsync<TEntity>(
                JsonConstants.DefaultOptions,
                cancellationToken)
            .ConfigureAwait(false);
        return item;
    }

    /// <summary>
    /// Perform a GET operation and deserialise the response.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to deserialise.</typeparam>
    /// <param name="url">The URL to GET.</param>
    /// <param name="options">The options to pass to the URL.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The deserialised entity if valid; <see langword="null"/> otherwise.</returns>
    public async IAsyncEnumerable<TEntity?> ListEntities<TEntity>(
        string url, 
        ICanvasOptions options, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
            where TEntity : class
    {
        var fullUrl = url;
        _logger.LogDebug("Listing {type} from {url}", typeof(TEntity).Name, fullUrl);
        while (!string.IsNullOrEmpty(fullUrl))
        {
            // Retrieve the response from the server
            var response = await Get(fullUrl, throwExceptionOnFailure: true, cancellationToken)
                .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode) yield break;

            // Deserialise the response into the entity type and return each item
            var items = await response.Content.ReadFromJsonAsync<TEntity[]>(
                    JsonConstants.DefaultOptions,
                    cancellationToken)
                .ConfigureAwait(false);
            if (items == null) yield break;
            foreach (var item in items) yield return item;

            // Retrieve the next link from the response headers and continue if it exists
            fullUrl = response.Headers.GetNextLink();
        }
    }

    /// <summary>
    /// Checks the response and applies throttling if we are getting near to our rate limit on Canvas.
    /// </summary>
    /// <param name="response">The response to check.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    private async Task ApplyThrottling(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        // Check if we have a value on the remaining rate limit and request cost
        var remaining = CheckForNumericHeaderValue(response, HeaderRateLimitRemaining);
        var cost = CheckForNumericHeaderValue(response, HeaderRequestCost);
        if ((remaining == null) || (cost == null)) return;

        var ratio = remaining.Value / cost.Value;
        if (ratio > ThrottlingThreshold) return;
        var delay = (ThrottlingThreshold - ratio) / ratio;
        _logger?.LogWarning("Applying throttling for {seconds} seconds", delay);
        await Task
            .Delay(TimeSpan.FromSeconds(delay), cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Check for a header and convert it to a numeric value.
    /// </summary>
    /// <param name="response">The response to check.</param>
    /// <param name="key">The key for the header.</param>
    /// <returns><see langword="null"/> if the header is missing or non-numeric; a <see langword="double"/> otherwise.</returns>
    private static double? CheckForNumericHeaderValue(HttpResponseMessage response, string key)
    {
        var header = response.Headers.FirstOrDefault(h => string.Equals(h.Key, key, StringComparison.OrdinalIgnoreCase));
        return (header.Value == null
            || !header.Value.Any()
            || !double.TryParse(header.Value.First(), System.Globalization.CultureInfo.InvariantCulture, out var value))
            ? null
            : value;
    }

    /// <summary>
    /// Checks the response and generates an exception if it has failed.
    /// </summary>
    /// <param name="url">The URL called.</param>
    /// <param name="response">The <see cref="HttpResponseMessage"/> instance.</param>
    /// <param name="codesToIgnore">The status codes to ignore.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    private static async Task CheckResponse(string url, HttpResponseMessage? response, HttpStatusCode[] codesToIgnore, CancellationToken cancellationToken)
    {
        if (response == null) throw new ConnectionException(url, "Something went wrong while calling Canvas");
        if (response.IsSuccessStatusCode || codesToIgnore.Contains(response.StatusCode)) return;

        // Handle the security status codes
        switch ((int)response.StatusCode)
        {
            case 401:
                throw new UnauthorizedException(url);

            case 403:
                throw new ForbiddenException(url);

            case 429:
                throw new RateLimitException(url);
        }

        using var stream = new MemoryStream();
        await response.Content.CopyToAsync(stream, cancellationToken).ConfigureAwait(false);

        // Canvas can return errors in a list format or a dictionary format
        await ParseErrorList(url, response, stream, cancellationToken).ConfigureAwait(false);
        await ParseErrorDictionary(url, response, stream, cancellationToken).ConfigureAwait(false);

        // If all else fails, just extract the content as a string and return that
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        throw new ConnectionException(url, $"Canvas returned a non-success response code [{response.StatusCode}]")
        {
            Content = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false),
        };
    }

    private static async Task ParseErrorDictionary(string url, HttpResponseMessage response, MemoryStream stream, CancellationToken cancellationToken)
    {
        stream.Seek(0, SeekOrigin.Begin);
        Exception? error = null;
        try
        {
            var errors = await JsonSerializer.DeserializeAsync<ErrorResponseDictionary>(
                    stream,
                    JsonConstants.DefaultOptions,
                    cancellationToken)
                .ConfigureAwait(false);
            Guard.IsNotNull(errors);
            error = new CanvasException(
                url,
                $"Canvas returned a non-success response code [{response.StatusCode}]",
                errors.Errors.SelectMany(kvp => kvp.Value));
        }
        catch
        {
            // Ignore any errors - this method is only checking for Canvas errors
        }
        if (error != null) throw error;
    }

    private static async Task<Exception?> ParseErrorList(string url, HttpResponseMessage response, MemoryStream stream, CancellationToken cancellationToken)
    {
        stream.Seek(0, SeekOrigin.Begin);
        Exception? error = null;
        try
        {
            var errors = await JsonSerializer.DeserializeAsync<ErrorResponseList>(
                    stream,
                    JsonConstants.DefaultOptions,
                    cancellationToken)
                .ConfigureAwait(false);
            Guard.IsNotNull(errors);
            error = new CanvasException(
                url,
                $"Canvas returned a non-success response code [{response.StatusCode}]",
                errors.Errors);
        }
        catch
        {
            // Ignore any errors - this method is only checking for Canvas errors
        }

        return error != null ? throw error : error;
    }

    /// <summary>
    /// Any Canvas errors in a list format.
    /// </summary>
    private record ErrorResponseList
    {
        public required CanvasError[] Errors { get; init; }
    }

    /// <summary>
    /// Any Canvas errors in a dictionary format.
    /// </summary>
    private record ErrorResponseDictionary
    {
        public required Dictionary<string, CanvasError[]> Errors { get; init; }
    }
}
