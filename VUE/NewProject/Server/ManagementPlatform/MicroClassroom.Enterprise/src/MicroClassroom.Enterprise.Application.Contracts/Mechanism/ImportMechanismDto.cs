using Magicodes.ExporterAndImporter.Core;

namespace MicroClassroom.Enterprise;

public class ImportMechanismDto
{
    [ImporterHeader(Name = "名称")]
    public string Name { get; private set; }

    [ImporterHeader(Name = "图像")]
    public string Image { get; private set; }

    [ImporterHeader(Name = "Slogo")]
    public string Slogo { get; private set; }

    [ImporterHeader(Name = "简介")]
    public string Introduce { get; private set; }

    [ImporterHeader(Name = "关于我们")]
    public string About { get; private set; }
}
