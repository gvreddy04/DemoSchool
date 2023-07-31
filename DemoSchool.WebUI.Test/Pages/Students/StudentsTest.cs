namespace DemoSchool.WebUI.Test.Pages.Students;

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

    [Fact]
    public void StudentsComponentRendersCorrectly_02()
    {
        // Arrange
        List<Student> students = new()
        {
            new Student
            {
                StudentId = 1,
                StudentName = "Test Student",
                GaurdianName = "Test Gaurdian",
                Email = "test@email.com",
                PhoneNumber = "9988776655",
                Gender = 1,
                CountryCode = 1,
                DOB = new DateTime(1990, 1, 1),
                Oid = Guid.NewGuid()
            }
        };
        _studentServiceMock.Setup(x => x.GetStudentsAsync()).ReturnsAsync(students);

        // Act
        var cut = this.RenderComponent<WebUI.Pages.Students.Students>();

        // Assert
        var rows = cut.FindAll("tbody tr");
        Assert.Equal(1, rows.Count);
    }
}
