using Magicodes.ExporterAndImporter.Core;
using System;

namespace MicroClassroom.Enterprise;

public class ImportCourseDto
{
    [ImporterHeader(Name = "课程名")]
    public string Name { get; set; }

    [ImporterHeader(Name = "图像")]
    public string Image { get; set; }

    [ImporterHeader(Name = "价格")]
    public decimal Price { get; set; }

    [ImporterHeader(Name = "是否付费")]
    public bool? HasPay { get; set; }

    [ImporterHeader(Name = "简介")]
    public string Introduce { get; set; }

    [ImporterHeader(Name = "开始时间")]
    public DateTime? StartAt { get; set; }

    [ImporterHeader(Name = "结束时间")]
    public DateTime? EndAt { get; set; }
}
