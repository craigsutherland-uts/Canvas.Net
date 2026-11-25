namespace Canvas.Client.Entities;

public readonly partial struct UserIdentifier
{

    /// <summary>
    /// The self identifier.
    /// </summary>
    public static readonly UserIdentifier Self = From("self");
}
