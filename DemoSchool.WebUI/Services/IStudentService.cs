namespace DemoSchool.WebUI.Services;

public interface IStudentService
{
    Task<int> CreateStudentAsync(Student student, StudentAddress address);
    Task<StudentVm> GetStudentByIdAsync(int id);
    Task<StudentsVm> GetStudentsAsync(int page, int size);
    Task<bool> UpdateStudentByOidAsync(Student student);
    Task<bool> UpdateStudentAddressByOidAsync(StudentAddress address);
}
