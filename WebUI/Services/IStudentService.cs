using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Services;

public interface IStudentService
{
    Task<int> CreateStudentAsync(Student student, StudentAddress address);
    Task<StudentVm> GetStudentByIdAsync(int id);
    Task<IEnumerable<Student>> GetStudentsAsync();
    Task<bool> UpdateStudentByOidAsync(Student student);
    Task<bool> UpdateStudentAddressAsync(StudentAddress address);
}
