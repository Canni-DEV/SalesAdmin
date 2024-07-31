namespace SalesAdminApi.CoreModule;

public record HealthCheck(int CheckNumber, DateTime StartTime, DateTime CheckDateTime, int HoursOnLine);
