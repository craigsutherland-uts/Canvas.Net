namespace Canvas.Client.Entities;

/// <summary>
/// A parameter for a request to the Canvas API.
/// </summary>
/// <param name="Name">The name of the parameter.</param>
/// <param name="Value">The value of the parameter.</param>
public sealed record Parameter(string Name, string Value);
