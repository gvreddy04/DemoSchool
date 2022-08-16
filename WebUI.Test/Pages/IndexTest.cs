namespace WebUI.Test.Pages;

public class IndexTest
{
    #region Verify Markup

    [Fact]
    public void IndexComponentRendersCorrectly()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var cut = ctx.RenderComponent<WebUI.Pages.Index>();

        // Assert
        var h1 = cut.Find("h1");
        h1.MarkupMatches("<h1>Hello, world!</h1>");
    }

    #endregion Verify Markup

    #region Semantic comparison

    [Fact]
    public void IndexComponentRendersCorrectly_02()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var cut = ctx.RenderComponent<WebUI.Pages.Index>();

        // Assert
        var a = cut.Find("a");
        a.MarkupMatches("<a target=\"_blank\" class=\"font-weight-bold link-dark\" href=\"https://go.microsoft.com/fwlink/?linkid=2148851\">brief survey</a>");
        // add new css class: test-link
        //a.MarkupMatches("<a target=\"_blank\" class:ignore href=\"https://go.microsoft.com/fwlink/?linkid=2148851\">brief survey</a>");
    }

    [Fact]
    public void IndexComponentRendersCorrectly_03()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var cut = ctx.RenderComponent<WebUI.Pages.Index>();

        // Assert
        var alertDiv = cut.Find(".alert");
        alertDiv.MarkupMatches("<div class=\"alert alert-secondary mt-4\"></div>");
        //alertDiv.MarkupMatches("<div class=\"alert alert-secondary mt-4\" diff:ignoreChildren></div>");
        //alertDiv.MarkupMatches("<div class:ignore diff:ignoreChildren></div>");
        //alertDiv.MarkupMatches("<div diff:ignoreAttributes diff:ignoreChildren></div>");
        //alertDiv.MarkupMatches("<div diff:ignore></div>");
    }

    #endregion  Semantic comparison
}
