namespace SalesAdminApi.CoreModule;

public static class CoreApiConfiguration
{
    public static IServiceCollection AddCoreService(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        });
        services.AddSingleton<ICoreService, CoreService>();
        return services;
    }

    public static WebApplication UseCore(this WebApplication app)
    {
        RouteGroupBuilder invoiceApi = app.MapGroup("/core");
        invoiceApi
            .MapGet("/health-check", (ICoreService service) => service.HealthCheck())
                .AddBaseEndpointConfiguration();
        return app;
    }

    public static RouteHandlerBuilder AddBaseEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        //Aplica configuraciones sobre los endpoint creados
        return builder;
    }

    public static IResult ExecuteEndpoint(Func<IResult> operation)
    {
        try
        {
            PreProcess();
            IResult result = operation();
            PostProcess(result);
            return result;
        }
        catch
        {
            return Results.Problem();
        }
    }
    public static IResult ExecuteEndpoint<T>(Func<T, IResult> operation, T parameters)
    {
        try
        {
            PreProcess();
            IResult result = operation(parameters);
            PostProcess(result);
            return result;
        }
        catch
        {
            return Results.Problem();
        }
    }
    private static bool PreProcess()
    {
        return true;
    }
    private static bool PostProcess(IResult result)
    {
        var a = result.ToString();
        return true;
    }
}