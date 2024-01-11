namespace MicroClassroom.Enterprise;

public static class EnterpriseDbProperties
{
    public static string DbTablePrefix { get; set; } = "Enterprise";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Enterprise";
}
