using CommunityToolkit.Diagnostics;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Canvas.Generator;

/// <summary>
/// The main engine for generating class for the Canvas REST API.
/// </summary>
public sealed class Engine
{
    private OpenApiDocument? _definition;

    /// <summary>
    /// An associated logger.
    /// </summary>
    public ILogger<Engine>? Logger { get; set; }

    /// <summary>
    /// The root namespace for generating entities.
    /// </summary>
    public string RootNamespace { get; set; } = "Canvas";

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
        foreach (var component in _definition.Components.Schemas)
        {
            await GenerateEntity(
                    Path.Combine(fullPath, "Entities"),
                    component,
                    cancellationToken)
                .ConfigureAwait(false);
            await GenerateDto(
                    Path.Combine(fullPath, "Dtos"),
                    component,
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
        CancellationToken cancellationToken)
    {
        // Retrieve the entity name
        var className = component.Key;
        Logger?.LogInformation("Generating entity {name}", className);
        var schema = component.Value;

        // Start the record definitions
        var entityDef = GenerateRecordDefinition(className, $"The {className.Humanize()} entity definition.", SyntaxKind.PublicKeyword);

        // Add each property
        var members = new List<MemberDeclarationSyntax>();
        var propertyNames = new List<string>();
        foreach (var property in schema.Properties)
        {
            members.Add(GenerateProperty(property, true, false));
            propertyNames.Add(property.Key.Dehumanize());
        }

        // Add the From method
        members.Add(GenerateFromDtoMethod(className, propertyNames));

        // Generate the compilation and add the usings, namespace, and record
        var compilation = GenerateCompilationUnit(
            entityDef
                .WithMembers(List(members))
                .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken)),
            QualifiedName(
                IdentifierName("System"),
                IdentifierName("Diagnostics")));

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

    private static MemberDeclarationSyntax GenerateFromDtoMethod(
        string className,
        List<string> propertyNames)
    {
        var dtoName = $"{className}Dto";
        var transferExpressions = propertyNames.SelectMany(pn => new SyntaxNodeOrToken[]{
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(pn),
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("dto"),
                    IdentifierName(pn))),
            Token(SyntaxKind.CommaToken),
        });
        var method = MethodDeclaration(
                    IdentifierName("className"),
                    Identifier("From"))
                .WithModifiers(
                    TokenList(
                        [Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.StaticKeyword)]))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                Identifier("dto"))
                            .WithType(
                                IdentifierName(dtoName)))))
                .WithBody(
                    Block(
                        SingletonList<StatementSyntax>(
                            ReturnStatement(
                                ObjectCreationExpression(
                                    IdentifierName(dtoName))
                                .WithInitializer(
                                    InitializerExpression(
                                        SyntaxKind.ObjectInitializerExpression,
                                        SeparatedList<ExpressionSyntax>(
                                            transferExpressions))))
                            .WithSemicolonToken(
                                MissingToken(SyntaxKind.SemicolonToken)))));
        return method;
    }

    private async Task GenerateDto(
        string rootPath,
        KeyValuePair<string, OpenApiSchema> component,
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
            properties.Add(GenerateProperty(property, false, true));
        }

        // Generate the compilation and add the usings, namespace, and record
        var compilation = GenerateCompilationUnit(
            dtoDef
                .WithMembers(List(properties))
                .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken)),
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

    private PropertyDeclarationSyntax GenerateProperty(
        KeyValuePair<string, OpenApiSchema> property,
        bool includeDocComments,
        bool includeJsonName)
    {
        var propertyName = property.Key.Dehumanize();
        Logger?.LogInformation(
            "=> C# name: {property}, JSON name: {json}, type: {type}",
            propertyName,
            property.Key,
            property.Value.Type);

        var type = GenerateType(property.Key, property.Value);

        var docComments = TriviaList();
        if (includeDocComments)
        {
            var xmlDoc = new List<SyntaxTrivia>();
            var hasNewLine = false;
            var exampleValue = RetrieveExampleValue(property.Value.Example);
            if (!string.IsNullOrEmpty(exampleValue))
            {
                xmlDoc.Insert(0, GenerateXmlDoc("example", exampleValue, !hasNewLine));
                hasNewLine = true;
            }
            if (!string.IsNullOrEmpty(property.Value.Description))
            {
                xmlDoc.Insert(0, GenerateXmlDoc("summary", property.Value.Description, !hasNewLine));
            }
            docComments = TriviaList(xmlDoc);
        }

        var definition = PropertyDeclaration(
                    NullableType(type),
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
        return definition;
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

    private static TypeSyntax GenerateType(string name, OpenApiSchema property)
    {
        if (property.Reference != null)
        {
            return ParseName(property.Reference.Id);
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
            { Type: "array" } => GenerateArrayType(name, property),
            _ => throw new GeneratorException(
                $"Unable to generate property '{name}': unknown type '{property.Type}'"),
        };
    }

    private static GenericNameSyntax GenerateArrayType(string name, OpenApiSchema property)
    {
        var itemType = property.Items.Reference != null
            ? ParseName(property.Items.Reference.Id)
            : GenerateType(name, property.Items);
        return GenericName(Identifier("IList"))
            .WithTypeArgumentList(
                TypeArgumentList(
                    SingletonSeparatedList(itemType)));
    }

    private CompilationUnitSyntax GenerateCompilationUnit(
        RecordDeclarationSyntax recordDef,
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
                            IdentifierName("Entities")))
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
    /// <param name="definitionPath">The path to the definition.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> instance.</param>
    /// <remarks>
    /// The definition MUST be in OpenAPI YAML format.
    /// </remarks>
    public async Task Initialise(
        string definitionPath,
        CancellationToken cancellationToken)
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

        _definition = result.OpenApiDocument;
    }

    [MemberNotNull(nameof(_definition))]
    private void EnsureInitialised()
    {
        if (_definition == null)
        {
            throw new GeneratorException("Unable to analyse components: engine has not been initialised");
        }
    }
}
