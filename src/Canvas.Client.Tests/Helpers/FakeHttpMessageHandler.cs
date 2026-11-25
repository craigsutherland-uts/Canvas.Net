using CommunityToolkit.Diagnostics;

namespace Canvas.Client.Tests.Helpers;

public sealed class FakeHttpMessageHandler
    : HttpMessageHandler
{
    public const string DefaultRootUrl = "http://testing.com";

    private readonly Dictionary<string, Message> _messages = [];

    public void AddResponse(string url, HttpResponseMessage message, string? rootUrl = DefaultRootUrl)
    {
        var fullUrl = string.IsNullOrEmpty(rootUrl)
            ? url
            : rootUrl + url;
        if (!_messages.TryGetValue(fullUrl, out var expectations))
        {
            expectations = new() { Url = fullUrl };
            _messages.Add(fullUrl, expectations);
        }
        expectations.AddMessage(message);
    }

    public void ValidateCalls()
    {
        var notCalled = _messages
            .Values
            .Where(m => !m.IsValid)
            .Select(m => $"Missing one or more calls to {m.Url}")
            .ToArray();
        if (notCalled.Length == 0) return;
        throw new Exception(string.Join(Environment.NewLine, notCalled));
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var url = request.RequestUri?.ToString();
        Guard.IsNotNull(url, "Request URI is missing");
        if (_messages.TryGetValue(url, out var resp)) return Task.FromResult(resp.Get());
        var allUrls = string.Join(", ", _messages.Keys);
        throw new Exception($"Unexpected URL {url} - configured URLs are {allUrls}");
    }

    private class Message
    {
        public int Count { get; private set; } = 0;
        public bool IsValid => Count == Messages.Count;

        public List<HttpResponseMessage> Messages { get; } = [];

        public required string Url { get; init; }

        public void AddMessage(HttpResponseMessage message)
        {
            Messages.Add(message);
        }

        public HttpResponseMessage Get()
        {
            return Count >= Messages.Count 
                ? throw new Exception($"No more messages pending for {Url}") 
                : Messages[Count++];
        }
    }
}