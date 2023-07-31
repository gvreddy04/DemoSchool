namespace DemoSchool.WebUI.Services;

public interface IStudentService
{
    Task<int> CreateStudentAsync(Student student, StudentAddress address);
    Task<StudentVm> GetStudentByIdAsync(int id);
    Task<IEnumerable<Student>> GetStudentsAsync();
    Task<bool> UpdateStudentByOidAsync(Student student);
    Task<bool> UpdateStudentAddressByOidAsync(StudentAddress address);
}
