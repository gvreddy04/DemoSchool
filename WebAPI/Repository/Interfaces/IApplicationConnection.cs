using System.Data;

namespace WebAPI.Repository.Interfaces;

public interface IApplicationConnection
{
    IDbConnection GetConnection();
}