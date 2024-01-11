using Magicodes.ExporterAndImporter.Core;

namespace MicroClassroom.Enterprise;

public class ImportTeacherDto
{
    [ImporterHeader(Name = "昵称")]
    public string Name { get; set; }

    [ImporterHeader(Name = "图像")]
    public string Image { get; set; }

    [ImporterHeader(Name = "简介")]
    public string Introduce { get; set; }
}
