using Vogen;

// Generates the IVogen<TWrapper, TPrimitive> interface, which can then be used in C# 14 extension members.
[assembly: VogenDefaults(
    staticAbstractsGeneration: StaticAbstractsGeneration.InstancesHaveInterfaceDefinition | StaticAbstractsGeneration.FactoryMethods)]