namespace DemoSchool.WebUI.Models;

public class StudentAddress
{
    public int StudentId { get; set; }
    public int StudentAddressId { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public Guid Oid { get; set; }
}
