namespace Canvas.Generator;

/// <summary>
/// An exception that occurred in the generator.
/// </summary>
/// <param name="message">The message for the error.</param>
[Serializable]
public class GeneratorException(string message)
    : Exception(message)
{
}