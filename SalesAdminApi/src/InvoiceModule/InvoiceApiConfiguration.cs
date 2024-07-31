namespace SalesAdminApi.InvoiceModule;

using Microsoft.AspNetCore.Authorization;
using SalesAdminApi.CoreModule;

public static class InvoiceApiConfiguration
{

    public static IServiceCollection AddInvoiceService(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddTransient<IInvoiceRepository, InvoiceRepository>(serv => new InvoiceRepository(config.GetConnectionString("FB") ?? ""));
        services.AddTransient<IInvoiceService, InvoiceService>();
        return services;
    }

    public static WebApplication UseInvoice(this WebApplication app)
    {
        RouteGroupBuilder invoicesApi = app.MapGroup("/invoices");
        invoicesApi
            .MapGet("/", [Authorize] async (IInvoiceService service) => await service.GetAll())
                .AddBaseEndpointConfiguration();
        invoicesApi
            .MapGet("/{id}", [Authorize] async (int id, IInvoiceService service) => await service.GetById(id))
                .AddBaseEndpointConfiguration();
        invoicesApi
            .MapPost("/filter", [Authorize] async (InvoiceFilterRequest filter, IInvoiceService service) => await service.GetByFilter(filter))
                .AddBaseEndpointConfiguration();
        return app;
    }
}