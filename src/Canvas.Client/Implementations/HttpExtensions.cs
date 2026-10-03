using System.Net.Http.Headers;

namespace Canvas.Client.Implementations;

internal static class HttpExtensions
{
    private const string LinkHeaderName = "Link";

    extension(HttpResponseHeaders headers)
    {
        /// <summary>
        /// Retrieve the next link from the response headers.
        /// </summary>
        /// <returns>The link if found; otherwise, <see langword="null"/>.</returns>
        public string? GetNextLink()
        {
            // Check if there is a link header and if it contains any values
            if (!headers.TryGetValues(LinkHeaderName, out var values)
                || !values.Any())
            {
                return null;
            }

            // Check for a link that ends with rel="next" 
            var links = string
                .Join(',', values)
                .Split([','], StringSplitOptions.RemoveEmptyEntries);
            var nextLink = links
                .FirstOrDefault(link => link.EndsWith("rel=\"next\"", StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrEmpty(nextLink))
            {
                return null;
            }

            // Extract the URL from the link
            var endIndex = nextLink.IndexOf('>', StringComparison.Ordinal);
            return nextLink[1..endIndex];
        }
    }
}
