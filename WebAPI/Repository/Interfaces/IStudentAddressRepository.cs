using WebAPI.Models;

namespace WebAPI.Repository.Interfaces;

public interface IStudentAddressRepository
{
    Task<(bool, int)> CreateStudentAddressAsync(StudentAddress studentAddress);
    Task<StudentAddress> GetStudentAddressByIdAsync(int id);
    Task<StudentAddress> GetStudentAddressByOidAsync(Guid oid);
    Task<StudentAddress> GetStudentAddressByStudentIdAsync(int studentId);
    Task<bool> UpdateStudentAddressByOidAsync(Guid oid, StudentAddress address);
}
