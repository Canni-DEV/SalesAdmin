using SalesAdminApi.InvoiceModule;
using SalesAdminApi.AccessModule;
using SalesAdminApi.CoreModule;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddCoreService();
builder.Services.AddAccessServices(builder.Configuration);
builder.Services.AddInvoiceService(builder.Configuration);

var app = builder.Build();

app.UseCore();
app.UseAccess();
app.UseInvoice();

app.Run();


