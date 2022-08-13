namespace WebAPI.Models;

public class StudentAddress
{
    public int StudentAddressId { get; set; }
    public int StudentId { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Guid Oid { get; set; }
}
