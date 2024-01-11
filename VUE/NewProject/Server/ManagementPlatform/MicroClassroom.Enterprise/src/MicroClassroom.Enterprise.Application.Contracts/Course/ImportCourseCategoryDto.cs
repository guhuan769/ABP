using Magicodes.ExporterAndImporter.Core;

namespace MicroClassroom.Enterprise;

public class ImportCourseCategoryDto
{
    [ImporterHeader(Name = "分类名")]
    public string Name { get; set; }

    [ImporterHeader(Name = "分类状态")]
    public int? Status { get; set; }
}
