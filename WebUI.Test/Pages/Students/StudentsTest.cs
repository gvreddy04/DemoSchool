using BlazorBootstrap;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Test.Pages.Students;

public class StudentsTest : TestContext
{
    private readonly Mock<IStudentService> _studentServiceMock;

    public StudentsTest()
    {
        _studentServiceMock = new Mock<IStudentService>();
        this.Services.AddSingleton<IStudentService>(_studentServiceMock.Object);
        this.Services.AddBlazorBootstrap();
    }

    [Fact]
    public void StudentsComponentRendersCorrectly()
    {
        // Arrange
        List<Student> students = new();
        _studentServiceMock.Setup(x => x.GetStudentsAsync()).ReturnsAsync(students);

        // Act
        var cut = this.RenderComponent<WebUI.Pages.Students.Students>();

        // Assert
        var h1 = cut.Find("h1");
        h1.MarkupMatches("<h1 diff:ignoreAttributes>Students</h1>");
    }
}
