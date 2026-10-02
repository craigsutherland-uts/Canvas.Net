using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security;

using Canvas.Generator.Models;

using CommunityToolkit.Diagnostics;

using Humanizer;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

using SharpYaml.Serialization;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Canvas.Generator;

/// <summary>
/// The main engine for generating class for the Canvas REST API.
/// </summary>
public sealed class Engine
{
    private ApiDefinition? _api;

    private OpenApiDocument? _schema;

    /// <summary>
    /// An associated logger.
    /// </summary>
    public ILogger<Engine>? Logger { get; set; }

    /// <summary>
    /// The root namespace for generating entities.
    /// </summary>
    public string RootNamespace { get; set; } = "Canvas.Client";

    /// <summary>
    /// Generates the code files to encapsulate the entities.
    /// </summary>
    /// <param name="rootPath">The root path to export the files.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    public async Task GenerateEntities(
        string rootPath,
        CancellationToken cancellationToken)
    {
        EnsureInitialised();

        // Ensure the folders exist
        var fullPath = Path.IsPathFullyQualified(rootPath)
            ? rootPath
            : Path.Combine(Environment.CurrentDirectory, rootPath);
        EnsureFolderExists(Path.Combine(fullPath, "Entities"));
        EnsureFolderExists(Path.Combine(fullPath, "Dtos"));

        // Generate each entity
        foreach (var component in _schema.Components.Schemas)
        {
            if (!_api.Entities.TryGetValue(component.Key, out var entity)) continue;
            await GenerateEntity(
                    Path.Combine(fullPath, "Entities"),
                    component,
                    entity,
                    cancellationToken)
                .ConfigureAwait(false);
            await GenerateDto(
                    Path.Combine(fullPath, "Dtos"),
                    component,
                    entity,
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }

    private void EnsureFolderExists(string fullPath)
    {
        if (!Directory.Exists(fullPath))
        {
            Logger?.LogInformation("Adding folder {path}", fullPath);
            Directory.CreateDirectory(fullPath);
        }
    }

    private async Task GenerateEntity(
        string rootPath,
        KeyValuePair<string, OpenApiSchema> component,
        EntityDefinition apiDefinition,
        CancellationToken cancellationToken)
    {
        // Retrieve the entity name
        var className = component.Key;
        Logger?.LogInformation("Generating entity {name}", className);
        var schema = component.Value;

        // Start the record definitions
        var entityDef = GenerateRecordDefinition(className, $"The {className.Humanize()} entity definition.", SyntaxKind.PublicKeyword);

        // Add the constructor
        var members = new List<MemberDeclarationSyntax>
        {
            GenerateField(IdentifierName($"{className}Dto"), "_source", false, true),
        };

        if (!string.IsNullOrEmpty(apiDefinition.Client))
        {
            members.AddRange(
                GenerateField(IdentifierName(apiDefinition.Client), "_client", true, false),
                GenerateConstructor(className, apiDefinition.Client));
        }
        members.Add(GenerateIsNewProperty(className));

        // Add each property
        var propertyNames = new Dictionary<string, (TypeSyntax, EntityPropertyDefinition?)>(StringComparer.Ordinal);
        foreach (var property in schema.Properties)
        {
            var propertyDefinition = apiDefinition.FindProperty(property.Key.Dehumanize());
            if (propertyDefinition?.Skip == true) continue;
            var propertyName = string.IsNullOrEmpty(propertyDefinition?.Alias)
                ? property.Key.Dehumanize()
                : propertyDefinition.Alias;
            var (propertyDef, typeDef) = GenerateProperty(propertyName, property, propertyDefinition, true, false, string.Empty);
            members.Add(propertyDef);
            propertyNames.Add(propertyName, (typeDef, propertyDefinition));
        }

        // Add the From methods
        members.AddRange(
            GenerateFromSingleDtoMethod(className, apiDefinition.Client, propertyNames),
            GenerateFromListDtoMethod(className, apiDefinition.Client, propertyNames));

        // Generate the compilation and add the usings, namespace, and record
        var compilation = GenerateCompilationUnit(
            entityDef
                .WithMembers(List(members))
                .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken)),
            "Entities");

        // Save the completed entity
        var outputPath = Path.Combine(rootPath, className + ".cs");
        var streamWriter = new StreamWriter(outputPath, append: false);
        await using var _ = streamWriter.ConfigureAwait(false);
        compilation
            .NormalizeWhitespace()
            .WriteTo(streamWriter);
        Logger?.LogInformation("Entity definition {className} saved to {path}", className, outputPath);
        await streamWriter.FlushAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private static MemberDeclarationSyntax GenerateIsNewProperty(string className)
    {
        return PropertyDeclaration(
                    PredefinedType(Token(SyntaxKind.BoolKeyword)),
                    Identifier("IsNew"))
                .WithModifiers(
                    TokenList(
                        Token(
                            TriviaList(
                                Trivia(
                                    DocumentationCommentTrivia(
                                        SyntaxKind.SingleLineDocumentationCommentTrivia,
                                        List<XmlNodeSyntax>(
                                            [
                                                XmlText()
                                                .WithTextTokens(
                                                    TokenList(
                                                        XmlTextLiteral(
                                                            TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList()))),
                                                XmlExampleElement(
                                                    SingletonList<XmlNodeSyntax>(
                                                        XmlText()
                                                        .WithTextTokens(
                                                            TokenList(
                                                                [
                                                                    XmlTextNewLine(TriviaList(), Environment.NewLine, Environment.NewLine, TriviaList()),
                                                                    XmlTextLiteral(
                                                                        TriviaList(
                                                                            DocumentationCommentExterior("    ///")),
                                                                        $" A flag indicating whether the {className} is new or retrieved from Canvas.",
                                                                        $" A flag indicating whether the {className} is new or retrieved from Canvas.",
                                                                        TriviaList()),
                                                                    XmlTextNewLine(TriviaList(), Environment.NewLine, Environment.NewLine, TriviaList()),
                                                                    XmlTextLiteral(
                                                                        TriviaList(
                                                                            DocumentationCommentExterior("    ///")),
                                                                        " ",
                                                                        " ",
                                                                        TriviaList())]))))
                                                .WithStartTag(XmlElementStartTag(XmlName(Identifier("summary"))))
                                                .WithEndTag(XmlElementEndTag(XmlName(Identifier("summary")))),
                                                XmlText()
                                                .WithTextTokens(
                                                    TokenList(
                                                        XmlTextNewLine(TriviaList(), Environment.NewLine, Environment.NewLine, TriviaList())))])))),
                            SyntaxKind.PublicKeyword,
                            TriviaList())))
                .WithExpressionBody(
                    ArrowExpressionClause(
                        BinaryExpression(
                            SyntaxKind.EqualsExpression,
                            IdentifierName("_source"),
                            LiteralExpression(SyntaxKind.NullLiteralExpression))))
                .WithSemicolonToken(
                    Token(SyntaxKind.SemicolonToken));
    }

    private static MemberDeclarationSyntax GenerateField(
        IdentifierNameSyntax variableType,
        string variableName,
        bool isReadOnly,
        bool isNullable)
    {
        var tokens = new List<SyntaxToken> { Token(SyntaxKind.PrivateKeyword) };
        if (isReadOnly) tokens.Add(Token(SyntaxKind.ReadOnlyKeyword));
        var variable = isNullable
            ? VariableDeclaration(NullableType(variableType))
            : VariableDeclaration(variableType);
        return FieldDeclaration(
                        variable
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(
                                    Identifier(variableName)))))
                    .WithModifiers(TokenList(tokens));
    }

    private async Task LoadApiDefinition(string apiPath, CancellationToken cancellationToken)
    {
        Guard.IsNotNullOrWhiteSpace(apiPath);

        // Check that the file exists
        var fullPath = Path.IsPathFullyQualified(apiPath)
            ? apiPath
            : Path.Combine(Environment.CurrentDirectory, apiPath);
        if (!File.Exists(fullPath))
        {
            throw new GeneratorException($"Unable to read '{fullPath}': unable to find file");
        }

        // Import the definition
        Logger?.LogInformation("Loading API definition from {path}", fullPath);
        var stream = new FileStream(
            apiPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            4096,
            useAsync: true);
        await using var _ = stream.ConfigureAwait(false);
        var settings = new SerializerSettings
        {
            NamingConvention = new CamelCaseNamingConvention(),
        };
        _api = new Serializer(settings).Deserialize<ApiDefinition>(stream)
            ?? throw new GeneratorException($"Unable to read '{fullPath}': could not deserialize");
    }

    private static MemberDeclarationSyntax GenerateConstructor(string className, string clientName)
    {
        return ConstructorDeclaration(
                    Identifier(className))
                .WithModifiers(
                    TokenList(
                        Token(SyntaxKind.InternalKeyword)))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                Identifier("client"))
                            .WithType(
                                IdentifierName(clientName)))))
                .WithBody(
                    Block(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Guard"),
                                    IdentifierName("IsNotNull")))
                            .WithArgumentList(
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(
                                            IdentifierName("client")))))),
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                IdentifierName("_client"),
                                IdentifierName("client")))));
    }


    private static MemberDeclarationSyntax GenerateFromListDtoMethod(
        string className,
        string? clientName,
        Dictionary<string, (TypeSyntax, EntityPropertyDefinition?)> propertyNames)
    {
        var dtoName = $"{className}Dto";

        var parameters = new List<ParameterSyntax>();
        var args = new List<ArgumentSyntax>();
        if (!string.IsNullOrEmpty(clientName))
        {
            parameters.Add(Parameter(Identifier("client")).WithType(IdentifierName(clientName)));
            args.Add(Argument(IdentifierName("client")));
        }
        parameters.Add(Parameter(Identifier("dto"))
            .WithType(
                NullableType(
                    GenericName(Identifier("IEnumerable"))
                    .WithTypeArgumentList(
                        TypeArgumentList(
                            SingletonSeparatedList<TypeSyntax>(IdentifierName(dtoName)))))));
        args.Add(Argument(IdentifierName("item")));

        var method = MethodDeclaration(
                    GenericName(
                        Identifier("IList"))
                    .WithTypeArgumentList(
                        TypeArgumentList(
                            SingletonSeparatedList<TypeSyntax>(
                                IdentifierName(className)))),
                    Identifier("From"))
                .WithModifiers(
                    TokenList(
                        [ Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.StaticKeyword)]))
                .WithParameterList(
                    ParameterList(
                        SeparatedList(parameters)))
                .WithBody(
                    Block(
                        IfStatement(
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("dto"),
                                LiteralExpression(
                                    SyntaxKind.NullLiteralExpression)),
                            ReturnStatement(
                                CollectionExpression())),
                        ReturnStatement(
                            CollectionExpression(
                                SingletonSeparatedList<CollectionElementSyntax>(
                                    SpreadElement(
                                        InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("dto"),
                                                IdentifierName("Select")))
                                        .WithArgumentList(
                                            ArgumentList(
                                                SingletonSeparatedList(
                                                    Argument(
                                                        SimpleLambdaExpression(
                                                            Parameter(Identifier("item")))
                                                        .WithExpressionBody(
                                                            PostfixUnaryExpression(
                                                                SyntaxKind.SuppressNullableWarningExpression,
                                                                InvocationExpression(
                                                                    IdentifierName("From"))
                                                                .WithArgumentList(
                                                                    ArgumentList(
                                                                        SeparatedList(args)))))))))))))));
        return method;
    }

    private static MemberDeclarationSyntax GenerateFromSingleDtoMethod(
        string className,
        string? clientName,
        Dictionary<string, (TypeSyntax, EntityPropertyDefinition?)> propertyNames)
    {
        var dtoName = $"{className}Dto";
        var transferExpressions = propertyNames.SelectMany(pn => new SyntaxNodeOrToken[]{
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(pn.Key),
                GenerateFromConversion(pn)),
            Token(SyntaxKind.CommaToken),
        }).ToList();
        transferExpressions.AddRange(AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName("_source"),
                IdentifierName("dto")),
            Token(SyntaxKind.CommaToken));
        var objectInitialisation = ObjectCreationExpression(IdentifierName(className))
            .WithInitializer(
            InitializerExpression(
                SyntaxKind.ObjectInitializerExpression,
                SeparatedList<ExpressionSyntax>(transferExpressions)));
        var parameters = new List<ParameterSyntax>();
        if (!string.IsNullOrEmpty(clientName))
        {
            parameters.Add(Parameter(Identifier("client")).WithType(IdentifierName(clientName)));
            objectInitialisation = objectInitialisation.WithArgumentList(
                ArgumentList(
                    SingletonSeparatedList(
                        Argument(IdentifierName("client")))));
        }
        parameters.Add(Parameter(Identifier("dto")).WithType(NullableType(IdentifierName(dtoName))));

        var method = MethodDeclaration(
                    NullableType(IdentifierName(className)),
                    Identifier("From"))
                .WithModifiers(
                    TokenList(
                        [Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.StaticKeyword)]))
                .WithParameterList(
                    ParameterList(
                        SeparatedList(parameters)))
                .WithBody(
                    Block(
                    IfStatement(
                        BinaryExpression(
                            SyntaxKind.EqualsExpression,
                            IdentifierName("dto"),
                            LiteralExpression(SyntaxKind.NullLiteralExpression)),
                        ReturnStatement(LiteralExpression(SyntaxKind.NullLiteralExpression))),
                    ReturnStatement(objectInitialisation)));
        return method;
    }

    private static ExpressionSyntax GenerateFromConversion(KeyValuePair<string, (TypeSyntax, EntityPropertyDefinition?)> pn)
    {
        var (typeDef, propertyDef) = pn.Value;
        ExpressionSyntax defaultValue = MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                IdentifierName("dto"),
                IdentifierName(pn.Key));
        if (!string.IsNullOrEmpty(propertyDef?.From)) defaultValue = InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                string.IsNullOrEmpty(propertyDef.FromType) 
                    ? string.IsNullOrEmpty(propertyDef.Type)
                        ? typeDef
                        : IdentifierName(propertyDef.Type)
                    : IdentifierName(propertyDef.FromType),
                IdentifierName(propertyDef.From)))
            .WithArgumentList(
            ArgumentList(
                SingletonSeparatedList(
                    Argument(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("dto"),
                            IdentifierName(pn.Key))))));
        return string.IsNullOrEmpty(propertyDef?.NullValue)
            ? defaultValue
            : ConditionalExpression(
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("dto"),
                        IdentifierName(pn.Key)),
                    LiteralExpression(
                        SyntaxKind.NullLiteralExpression)),
                ParseExpression(propertyDef.NullValue),
                defaultValue);
    }

    private async Task GenerateDto(
        string rootPath,
        KeyValuePair<string, OpenApiSchema> component,
        EntityDefinition apiDefinition,
        CancellationToken cancellationToken)
    {
        // Retrieve the entity name
        var className = $"{component.Key}Dto";
        Logger?.LogInformation("Generating DTO {name}", className);
        var schema = component.Value;

        // Start the record definitions
        var dtoDef = GenerateRecordDefinition(className, $"A DTO for transferring {component.Key.Humanize()} entities.", SyntaxKind.InternalKeyword);

        // Add each property
        var properties = new List<MemberDeclarationSyntax>();
        foreach (var property in schema.Properties)
        {
            var propertyDefinition = apiDefinition.FindProperty(property.Key.Dehumanize());
            if (propertyDefinition?.Skip == true) continue;
            var propertyName = string.IsNullOrEmpty(propertyDefinition?.Alias)
                ? property.Key.Dehumanize()
                : propertyDefinition.Alias;
            var (propertyDef, _) = GenerateProperty(
                propertyName, 
                property, 
                new EntityPropertyDefinition { Type = propertyDefinition?.Dto },
                false, 
                true, 
                "Dto");
            properties.Add(propertyDef);
        }

        // Generate the compilation and add the usings, namespace, and record
        var compilation = GenerateCompilationUnit(
            dtoDef
                .WithMembers(List(properties))
                .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken)),
            "Dtos",
            QualifiedName(
                QualifiedName(
                    QualifiedName(
                        IdentifierName("System"),
                        IdentifierName("Text")),
                    IdentifierName("Json")),
                IdentifierName("Serialization")));

        // Save the completed entity
        var outputPath = Path.Combine(rootPath, className + ".cs");
        var streamWriter = new StreamWriter(outputPath, append: false);
        await using var _ = streamWriter.ConfigureAwait(false);
        compilation
            .NormalizeWhitespace()
            .WriteTo(streamWriter);
        Logger?.LogInformation("Entity definition {className} saved to {path}", className, outputPath);
        await streamWriter.FlushAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private (PropertyDeclarationSyntax, TypeSyntax) GenerateProperty(
        string propertyName,
        KeyValuePair<string, OpenApiSchema> property,
        EntityPropertyDefinition? propertyDefinition,
        bool includeDocComments,
        bool includeJsonName,
        string classNameSuffix)
    {
        var type = string.IsNullOrEmpty(propertyDefinition?.Type)
            ? GenerateType(property.Key, property.Value, classNameSuffix)
            : ParseName(propertyDefinition.Type);
        var dataType = type;
        var docComments = TriviaList();
        if (includeDocComments)
        {
            var xmlDoc = new List<SyntaxTrivia>();
            var hasNewLine = false;
            var exampleValue = RetrieveExampleValue(property.Value.Example);
            if (!string.IsNullOrEmpty(exampleValue))
            {
                xmlDoc.Insert(0, GenerateXmlDoc("example", SecurityElement.Escape(exampleValue), !hasNewLine));
                hasNewLine = true;
            }
            xmlDoc.Insert(0, GenerateXmlDoc("summary", SecurityElement.Escape(property.Value.Description ?? string.Empty), !hasNewLine));
            docComments = TriviaList(xmlDoc);
        }

        if (propertyDefinition?.Nullable != false) type = NullableType(type);
        var definition = PropertyDeclaration(
                    type,
                    Identifier(propertyName))
                .WithModifiers(
                    TokenList(Token(docComments, SyntaxKind.PublicKeyword, TriviaList())))
                .WithAccessorList(
                    AccessorList(
                        List(
                            [
                                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                                AccessorDeclaration(SyntaxKind.InitAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                            ])));
        if (includeJsonName && !string.Equals(propertyName, property.Key, StringComparison.OrdinalIgnoreCase))
        {
            definition = definition.WithAttributeLists(SingletonList(
                        AttributeList(
                            SingletonSeparatedList(
                                Attribute(
                                    IdentifierName("JsonPropertyName"))
                                .WithArgumentList(
                                    AttributeArgumentList(
                                        SingletonSeparatedList(
                                            AttributeArgument(
                                                LiteralExpression(
                                                    SyntaxKind.StringLiteralExpression,
                                                    Literal(property.Key))))))))));
        }
        return (definition, dataType);
    }

    private static string? RetrieveExampleValue(IOpenApiAny example)
    {
        if (example == null) return null;
        return example switch
        {
            OpenApiString x => $"\"{x.Value}\"",
            OpenApiInteger x => x.Value.ToString(CultureInfo.CurrentCulture),
            OpenApiLong x => x.Value.ToString(CultureInfo.CurrentCulture),
            OpenApiFloat x => x.Value.ToString(CultureInfo.CurrentCulture),
            OpenApiDouble x => x.Value.ToString(CultureInfo.CurrentCulture),
            OpenApiBoolean x => x.Value.ToString(CultureInfo.CurrentCulture),
            OpenApiDateTime x => x.Value.ToString("O"),
            OpenApiArray x => "[" + string.Join(", ", x.Select(i => RetrieveExampleValue(i)).Where(i => i != null)) + "]",
            OpenApiObject x => null,
            OpenApiNull => null,
            _ => throw new NotSupportedException($"Unsupported OpenAPI value type: {example.GetType().Name}"),
        };
    }

    private TypeSyntax GenerateType(string name, OpenApiSchema property, string classNameSuffix)
    {
        if (property.Reference != null)
        {
            var propertyName = property.Reference.Id;
            if (_api?.TypeAliases.TryGetValue(propertyName, out var alias) == true) propertyName = alias ?? propertyName;
            return ParseName(propertyName + classNameSuffix);
        }

        return property switch
        {
            { Type: "string", Format: "date-time" } => IdentifierName("DateTime"),
            { Type: "string" } => PredefinedType(Token(SyntaxKind.StringKeyword)),
            { Type: "integer" } => PredefinedType(Token(SyntaxKind.IntKeyword)),
            { Type: "int64" } => PredefinedType(Token(SyntaxKind.LongKeyword)),
            { Type: "boolean" } => PredefinedType(Token(SyntaxKind.BoolKeyword)),
            { Type: "number", Format: "float" } => PredefinedType(Token(SyntaxKind.FloatKeyword)),
            { Type: "number" } => PredefinedType(Token(SyntaxKind.DoubleKeyword)),
            { Type: "object" } => PredefinedType(Token(SyntaxKind.ObjectKeyword)),
            { Type: "array" } => GenerateArrayType(name, property, classNameSuffix),
            _ => throw new GeneratorException(
                $"Unable to generate property '{name}': unknown type '{property.Type}'"),
        };
    }

    private GenericNameSyntax GenerateArrayType(string name, OpenApiSchema property, string classNameSuffix)
    {
        var itemType = property.Items.Reference != null
            ? ParseName(property.Items.Reference.Id + classNameSuffix)
            : GenerateType(name, property.Items, classNameSuffix);
        return GenericName(Identifier("IList"))
            .WithTypeArgumentList(
                TypeArgumentList(
                    SingletonSeparatedList(itemType)));
    }

    private CompilationUnitSyntax GenerateCompilationUnit(
        RecordDeclarationSyntax recordDef,
        string leafNamespace,
        params QualifiedNameSyntax[] namespacesToUse)
    {
        return CompilationUnit()
            .WithUsings(
                List(namespacesToUse.Select(UsingDirective)))
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    FileScopedNamespaceDeclaration(
                        QualifiedName(
                            ParseName(RootNamespace),
                            IdentifierName(leafNamespace)))
                    .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(recordDef))));
    }

    private static RecordDeclarationSyntax GenerateRecordDefinition(
        string name,
        string summary,
        SyntaxKind accessLevel)
    {
        return RecordDeclaration(
                SyntaxKind.RecordDeclaration,
                Token(SyntaxKind.RecordKeyword),
                Identifier(name))
            .WithModifiers(
                TokenList(
                    Token(
                        TriviaList(GenerateXmlDoc("summary", summary, true)),
                        accessLevel,
                        TriviaList()),
                    Token(SyntaxKind.SealedKeyword),
                    Token(SyntaxKind.PartialKeyword)
                ))
            .WithOpenBraceToken(
                Token(SyntaxKind.OpenBraceToken));
    }

    private static SyntaxTrivia GenerateXmlDoc(string tag, string text, bool appendNewLine)
    {
        var xml = new List<XmlNodeSyntax>
        {
            XmlText()
            .WithTextTokens(
                TokenList(
                    XmlTextLiteral(
                        TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList()))),
            XmlExampleElement(
                SingletonList<XmlNodeSyntax>(
                    XmlText()
                    .WithTextTokens(
                        TokenList(
                            [
                                XmlTextNewLine(
                                    TriviaList(),
                                    Environment.NewLine,
                                    Environment.NewLine,
                                    TriviaList()),
                                XmlTextLiteral(
                                    TriviaList(DocumentationCommentExterior("///")),
                                    " " + text,
                                    " " + text,
                                    TriviaList()),
                                XmlTextNewLine(
                                    TriviaList(),
                                    Environment.NewLine,
                                    Environment.NewLine,
                                    TriviaList()),
                                XmlTextLiteral(
                                    TriviaList(DocumentationCommentExterior("///")),
                                    " ",
                                    " ",
                                    TriviaList()),
                            ]))))
            .WithStartTag(
                XmlElementStartTag(
                    XmlName(Identifier(tag))))
            .WithEndTag(
                XmlElementEndTag(
                    XmlName(Identifier(tag)))),
        };
        if (appendNewLine)
        {
            xml.Add(XmlText()
                .WithTextTokens(
                    TokenList(
                        XmlTextNewLine(
                            TriviaList(),
                            Environment.NewLine,
                            Environment.NewLine,
                            TriviaList()))));
        }
        return Trivia(
            DocumentationCommentTrivia(
                SyntaxKind.SingleLineDocumentationCommentTrivia,
                List(xml)));
    }

    /// <summary>
    /// Initialises the engine.
    /// </summary>
    /// <param name="schemaPath">The path to the schema definition.</param>
    /// <param name="apiPath">The path to the API definition.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> instance.</param>
    /// <remarks>
    /// The definition MUST be in OpenAPI YAML format.
    /// </remarks>
    public async Task Initialise(
        string schemaPath,
        string apiPath,
        CancellationToken cancellationToken)
    {
        await LoadSchemaDefinition(schemaPath, cancellationToken).ConfigureAwait(false);
        await LoadApiDefinition(apiPath, cancellationToken).ConfigureAwait(false);
    }

    private async Task LoadSchemaDefinition(string definitionPath, CancellationToken cancellationToken)
    {
        Guard.IsNotNullOrWhiteSpace(definitionPath);

        // Check that the file exists
        var fullPath = Path.IsPathFullyQualified(definitionPath)
            ? definitionPath
            : Path.Combine(Environment.CurrentDirectory, definitionPath);
        if (!File.Exists(fullPath))
        {
            throw new GeneratorException($"Unable to read '{fullPath}': unable to find file");
        }

        // Import the definition
        var stream = new FileStream(
            fullPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            4096,
            useAsync: true);
        await using var _ = stream.ConfigureAwait(false);
        var reader = new OpenApiStreamReader();
        var result = await reader.ReadAsync(
                stream,
                cancellationToken)
            .ConfigureAwait(false)
            ?? throw new GeneratorException($"Unable to read '{fullPath}': reader returned an empty result");

        // Validate that the import was successful
        if (result.OpenApiDiagnostic.Errors.Count > 0)
        {
            var errors = string.Join(
                Environment.NewLine,
                result.OpenApiDiagnostic.Errors.Select(m => "* " + m.Message));
            throw new GeneratorException(
                $"Unable to read '{fullPath}': invalid OpenAPI definition" +
                Environment.NewLine +
                errors);
        }

        _schema = result.OpenApiDocument;
    }

    [MemberNotNull(nameof(_api))]
    [MemberNotNull(nameof(_schema))]
    private void EnsureInitialised()
    {
        if (_schema == null || _api == null)
        {
            throw new GeneratorException("Unable to analyse components: engine has not been initialised");
        }
    }
}
