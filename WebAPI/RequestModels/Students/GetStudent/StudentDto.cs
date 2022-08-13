using WebAPI.Common.Mappings;
using WebAPI.Models;

namespace WebAPI.RequestModels.Students.GetStudent;

public class StudentDto : IMapFrom<Student>
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = null!;
    public string GaurdianName { get; set; } = null!;
    public int Gender { get; set; }
    public DateTime DOB { get; set; }
    public int CountryCode { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid Oid { get; set; }
}
