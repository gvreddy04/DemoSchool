﻿using DemoSchool.WebUI.ViewModels;

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
        StudentsVm studentsVm = new() { Data = new List<Student>(), Count = 0 };
        _studentServiceMock.Setup(x => x.GetStudentsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(studentsVm);

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
        StudentsVm studentsVm = new() { Data = students, Count = 1 };
        _studentServiceMock.Setup(x => x.GetStudentsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(studentsVm);

        // Act
        var cut = this.RenderComponent<WebUI.Pages.Students.Students>();

        // Assert
        var rows = cut.FindAll("tbody tr");
        Assert.Equal(1, rows.Count);
    }
}
