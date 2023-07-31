using Dapper;
using System.Data;
using WebAPI.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly ILogger<StudentRepository> _logger;
    private readonly IApplicationConnection _applicationConnection;

    public StudentRepository(ILogger<StudentRepository> logger, IApplicationConnection applicationConnection)
    {
        _logger = logger;
        _applicationConnection = applicationConnection;
    }

    public async Task<(bool, int)> CreateStudentAsync(Student student)
    {
        string procedureName = "[dbo].[CreateStudent]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new
                {
                    StudentName = student.StudentName,
                    GaurdianName = student.GaurdianName,
                    Gender = student.Gender,
                    DOB = student.DOB,
                    CountryCode = student.CountryCode,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    CreatedBy = student.CreatedBy
                };
                var id = await connection.ExecuteScalarAsync<int>(procedureName, param: param, commandType: CommandType.StoredProcedure);
                return (true, id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentRepository.CreateStudentAsync(), StudentName={student.StudentName}, GaurdianName={student.GaurdianName}, gender={student.Gender}, dob={student.DOB}, CountryCode={student.CountryCode}, PhoneNumber={student.PhoneNumber}, Email={student.Email}, createdBy ={student.CreatedBy}");
            return (false, -1);
        }
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        string procedureName = "[dbo].[GetStudentById]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new { StudentId = id };
                return await connection.QuerySingleOrDefaultAsync<Student>(procedureName, param: param, commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentRepository.GetStudentByIdAsync(), id={id}");
            return null;
        }
    }

    public async Task<Student> GetStudentByOidAsync(Guid oid)
    {
        string procedureName = "[dbo].[GetStudentByOid]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new { Oid = oid };
                return await connection.QuerySingleOrDefaultAsync<Student>(procedureName, param: param, commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentRepository.GetStudentByOidAsync(), oid={oid}");
            return null;
        }
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        string procedureName = "[dbo].[GetStudents]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                return await connection.QueryAsync<Student>(procedureName, commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentRepository.GetStudentsAsync()");
            return new List<Student>();
        }
    }

    public async Task<bool> UpdateStudentByOidAsync(Guid oid, Student student)
    {
        string procedureName = "[dbo].[UpdateStudentByOid]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new
                {
                    Oid = oid,
                    StudentName = student.StudentName,
                    GaurdianName = student.GaurdianName,
                    Gender = student.Gender,
                    DOB = student.DOB,
                    CountryCode = student.CountryCode,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    ModifiedBy = student.ModifiedBy
                };
                _ = await connection.ExecuteAsync(procedureName, param: param, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentRepository.UpdateStudentByOidAsync(), oid={oid}, StudentName={student.StudentName}, GaurdianName={student.GaurdianName}, CountryCode={student.CountryCode}, PhoneNumber={student.PhoneNumber}, Email={student.Email}, modifiedBy ={student.ModifiedBy}");
            return false;
        }
    }
}