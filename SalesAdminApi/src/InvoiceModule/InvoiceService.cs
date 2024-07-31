namespace SalesAdminApi.InvoiceModule;

public interface IInvoiceService
{
    Task<IResult> GetById(int id);
    Task<IResult> GetAll();
    Task<IResult> GetByFilter(InvoiceFilterRequest filter);
}

public class InvoiceService(IInvoiceRepository repository) : IInvoiceService
{
    public async Task<IResult> GetAll()
    {
        var invoices = await repository.GetAll();
        return invoices is null ? Results.NotFound(invoices) : Results.Ok(invoices);
    }

    public async Task<IResult> GetById(int id)
    {
        var invoice = await repository.GetById(id);
        return invoice is null ? Results.NotFound(invoice) : Results.Ok(invoice);
    }

    public async Task<IResult> GetByFilter(InvoiceFilterRequest filter)
    {
        var invoice = await repository.GetByFilter(filter);
        return invoice is null ? Results.NotFound(invoice) : Results.Ok(invoice);
    }
}