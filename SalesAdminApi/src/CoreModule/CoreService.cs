namespace SalesAdminApi.CoreModule;

public interface ICoreService
{
    IResult HealthCheck();
}

public class CoreService : ICoreService
{
    private int checkNumber = 0;
    private readonly DateTime startDateTime = DateTime.Now;

    public IResult HealthCheck()
    {
        checkNumber++;
        DateTime now = DateTime.Now;
        int hours = now.Subtract(startDateTime).Hours;
        return Results.Ok(new HealthCheck(checkNumber, startDateTime, DateTime.Now, hours));
    }
}