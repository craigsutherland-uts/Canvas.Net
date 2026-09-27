namespace Canvas.Client.Implementations;

/// <summary>
/// Marks a client so that it can be discovered and added to a service collection.
/// </summary>
/// <typeparam name="TClient">The client interface that the client implements.</typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
internal class CanvasClientAttribute<TClient> 
    : Attribute
{
}
