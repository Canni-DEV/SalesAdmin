using Dapper;
using Microsoft.Data.SqlClient;

namespace SalesAdminApi.AccessModule;

public interface IAccessRepository
{
    Task<UserDto> GetUserById(int id);
}

public class AccessRepository(string connectionString) : IAccessRepository
{
    public async Task<UserDto> GetUserById(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            return await connection.QueryFirstAsync<UserDto>("SELECT  TOP 1 * FROM Users");
        }
    }
}