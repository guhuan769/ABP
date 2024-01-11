namespace MicroClassroom.Enterprise;

public class CreateMechanismInput
{
    public string Name { get; set; }

    public string Pinyin { get; set; }

    public string Image { get; set; }

    public string Slogo { get; set; }

    public string Introduce { get; set; }

    public int? Grade { get; set; }

    public string About { get; set; }

    /// <summary>
    /// 多租户连接字符串
    /// </summary>
    public string ConnectionString { get; set; }
}
