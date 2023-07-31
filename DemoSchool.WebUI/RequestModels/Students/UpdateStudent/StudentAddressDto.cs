namespace DemoSchool.WebUI.RequestModels.Students.UpdateStudent;

public class StudentAddressDto : IMapFrom<StudentAddress>
{
    public int StudentId { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public Guid Oid { get; set; }
}