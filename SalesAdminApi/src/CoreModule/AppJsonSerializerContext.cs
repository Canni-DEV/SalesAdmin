namespace SalesAdminApi.CoreModule;
//TODO: esta configuracion json malvada, hay que cambiarla. Tiene que haber una mejor forma.
using System.Text.Json.Serialization;
using SalesAdminApi.AccessModule;
using SalesAdminApi.InvoiceModule;

[JsonSerializable(typeof(Invoice[]))]
[JsonSerializable(typeof(InvoiceFilterRequest))]
[JsonSerializable(typeof(UserDto))]
[JsonSerializable(typeof(HealthCheck))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}