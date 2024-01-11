namespace Elon.BasicInfo;

public static class BasicInfoDbProperties
{
    public static string DbTablePrefix { get; set; } = "BasicInfo";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "BasicInfo";
}
