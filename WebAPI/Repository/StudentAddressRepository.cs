using Dapper;
using System.Data;
using WebAPI.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository;

public class StudentAddressRepository : IStudentAddressRepository
{
    private readonly ILogger<StudentAddressRepository> _logger;
    private readonly IApplicationConnection _applicationConnection;

    public StudentAddressRepository(ILogger<StudentAddressRepository> logger, IApplicationConnection applicationConnection)
    {
        _logger = logger;
        _applicationConnection = applicationConnection;
    }

    public async Task<(bool, int)> CreateStudentAddressAsync(StudentAddress address)
    {
        string procedureName = "[dbo].[CreateStudentAddress]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new
                {
                    StudentId = address.StudentId,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    AddressLine3 = address.AddressLine3,
                    City = address.City,
                    State = address.State,
                    Country = address.Country,
                    ZipCode = address.ZipCode,
                    CreatedBy = address.CreatedBy
                };
                var id = await connection.ExecuteScalarAsync<int>(procedureName, param: param, commandType: CommandType.StoredProcedure);
                return (true, id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentAddressRepository.CreateStudentAddressAsync(), studentId={address.StudentId}, createdBy ={address.CreatedBy}");
            return (false, -1);
        }
    }

    public async Task<StudentAddress> GetStudentAddressByIdAsync(int id)
    {
        string procedureName = "[dbo].[GetStudentAddressById]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new { StudentAddressId = id };
                return await connection.QuerySingleOrDefaultAsync<StudentAddress>(procedureName, param: param, commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentAddressRepository.GetStudentAddressByIdAsync(), id={id}");
            return null;
        }
    }

    public async Task<StudentAddress> GetStudentAddressByOidAsync(Guid oid)
    {
        string procedureName = "[dbo].[GetStudentAddressByOid]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new { Oid = oid };
                return await connection.QuerySingleOrDefaultAsync<StudentAddress>(procedureName, param: param, commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentAddressRepository.GetStudentAddressByOidAsync(), oid={oid}");
            return null;
        }
    }

    public async Task<StudentAddress> GetStudentAddressByStudentIdAsync(int studentId)
    {
        string procedureName = "[dbo].[GetStudentAddressByStudentId]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new { StudentId = studentId };
                return await connection.QuerySingleOrDefaultAsync<StudentAddress>(procedureName, param: param, commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentAddressRepository.GetStudentAddressByStudentIdAsync(), studentId={studentId}");
            return null;
        }
    }

    public async Task<bool> UpdateStudentAddressByOidAsync(Guid oid, StudentAddress address)
    {
        string procedureName = "[dbo].[UpdateStudentAddressByOid]";
        try
        {
            using (var connection = _applicationConnection.GetConnection())
            {
                var param = new
                {
                    Oid = oid,
                    StudentId = address.StudentId,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    AddressLine3 = address.AddressLine3,
                    City = address.City,
                    State = address.State,
                    Country = address.Country,
                    ZipCode = address.ZipCode,
                    ModifiedBy = address.ModifiedBy
                };
                _ = await connection.ExecuteAsync(procedureName, param: param, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"ErrorMessage={ex.Message}, Source=StudentAddressRepository.UpdateStudentAddressByOidAsync(), oid={oid}, studentId={address.StudentId}, createdBy ={address.CreatedBy}");
            return false;
        }
    }
}
