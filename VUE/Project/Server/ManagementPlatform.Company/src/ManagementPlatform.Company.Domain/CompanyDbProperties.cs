namespace ManagementPlatform.Company;

public static class CompanyDbProperties
{
    public static string DbTablePrefix { get; set; } = "Company";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Company";
}
