using WebAPI.Common.Mappings;
using WebAPI.Models;

namespace WebAPI.RequestModels.Students.GetStudent;

public class StudentAddressDto : IMapFrom<StudentAddress>
{
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
