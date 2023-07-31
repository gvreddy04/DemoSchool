namespace DemoSchool.WebAPI.Models;

public class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = null!;
    public string GaurdianName { get; set; } = null!;
    public int Gender { get; set; }
    public DateTime DOB { get; set; }
    public int CountryCode { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Guid Oid { get; set; }
}
