using Canvas.Client.Entities;
using Canvas.Client.Options;

namespace Canvas.Client.Tests;

/// <summary>
/// Unit tests for <see cref="CanvasOptions"/>.
/// </summary>
public class CanvasOptionsTests
{
    [Fact]
    public void NewGeneratesEmptyOptions()
    {
        // Arrange
        var opts = CanvasOptions.New();

        // Act
        var query = opts.ToQueryString();

        // Assert
        query.ShouldBe(string.Empty);
    }

    [Fact]
    public void WithPageSizeAddsQueryParameter()
    {
        // Arrange
        var opts = CanvasOptions
            .New()
            .WithPageSize(10);

        // Act
        var query = opts.ToQueryString();

        // Assert
        query.ShouldBe("?per_page=10");
    }

    [Fact]
    public void WithMasqueradingFromUserIdentifierAddsQueryParameter()
    {
        // Arrange
        var opts = CanvasOptions
            .New()
            .WithMasquerading(UserIdentifier.Self);

        // Act
        var query = opts.ToQueryString();

        // Assert
        query.ShouldBe("?as_user_id=self");
    }

    [Fact]
    public void WithMasqueradingFromUserAddsQueryParameter()
    {
        // Arrange
        var user = new User(A.Fake<ICanvasClient>())
        {
            Id = UserIdentifier.From(123)
        };
        var opts = CanvasOptions
            .New()
            .WithMasquerading(user);

        // Act
        var query = opts.ToQueryString();

        // Assert
        query.ShouldBe("?as_user_id=123");
    }

    [Theory]
    [InlineData(CourseInclude.None, "")]
    [InlineData(CourseInclude.Account, "?include%5B%5D=account")]
    [InlineData(CourseInclude.CourseImage, "?include%5B%5D=course_image")]
    [InlineData(CourseInclude.Account | CourseInclude.Term, "?include%5B%5D=term&include%5B%5D=account")]
    public void WithIncludeAsFlagsAddsRevelantQueryParameters(CourseInclude include, string expected)
    {
        // Arrange
        var opts = CanvasOptions
            .New()
            .WithInclude(include);

        // Act
        var query = opts.ToQueryString();

        // Assert
        query.ShouldBe(expected);
    }

    [Theory]
    [InlineData(SingleOptionInclude.First, "?include%5B%5D=first")]
    [InlineData(SingleOptionInclude.Second, "?include%5B%5D=second")]
    [InlineData(SingleOptionInclude.ThirdOption, "?include%5B%5D=third_option")]
    public void WithIncludeAsNonFlagAddsRevelantQueryParameter(SingleOptionInclude include, string expected)
    {
        // Arrange
        var opts = CanvasOptions
            .New()
            .WithInclude(include);

        // Act
        var query = opts.ToQueryString();

        // Assert
        query.ShouldBe(expected);
    }

    public enum SingleOptionInclude
    {
        First,
        Second,
        ThirdOption,
    }
}