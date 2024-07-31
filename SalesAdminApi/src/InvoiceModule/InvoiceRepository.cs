namespace SalesAdminApi.InvoiceModule;

using Dapper;
using Microsoft.Data.SqlClient;

public interface IInvoiceRepository
{
    Task<Invoice?> GetById(int id);
    Task<Invoice[]> GetAll();
    Task<Invoice[]> GetByFilter(InvoiceFilterRequest filter);
}

public class InvoiceRepository(string connectionString) : IInvoiceRepository
{
    public async Task<Invoice[]> GetAll()
    {
        await Task.Delay(1);
        var random = new Random();
        return Enumerable.Range(1, 100)
            .Select(i => new Invoice
            {
                Id = i,
                Description = $"Factura aleatoria {i}" + random.Next().ToString()
            }).ToArray();
    }

    public async Task<Invoice[]> GetByFilter(InvoiceFilterRequest filter)
    {
        await Task.Delay(1);
        var random = new Random();
        return Enumerable.Range(1, 100 + filter.Id)
            .Select(i => new Invoice
            {
                Id = i,
                Description = $"Factura aleatoria {i}" + random.Next().ToString()
            }).ToArray();
    }

    public async Task<Invoice?> GetById(int id)
    {
        await Task.Delay(1);
        return Enumerable.Range(1, 100)
            .Select(i => new Invoice
            {
                Id = i,
                Description = $"Factura aleatoria {i}"
            }).ToList().ElementAtOrDefault(id);
    }

    public async Task<IEnumerable<Invoice>> GetAllTodosAsync()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            return await connection.QueryAsync<Invoice>("SELECT * FROM Invoice");
        }
    }
}