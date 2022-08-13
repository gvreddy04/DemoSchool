namespace WebAPI.RequestModels.Students.GetStudent;

public class StudentVm
{
    public StudentDto Student { get; set; } = null!;
    public StudentAddressDto Address { get; set; } = null!;
}
