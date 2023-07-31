namespace DemoSchool.WebAPI.Repository.Interfaces;

public interface IApplicationConnection
{
    IDbConnection GetConnection();
}