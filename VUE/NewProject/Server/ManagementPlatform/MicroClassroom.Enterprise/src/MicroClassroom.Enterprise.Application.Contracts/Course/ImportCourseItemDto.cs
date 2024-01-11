using Magicodes.ExporterAndImporter.Core;
using System;

namespace MicroClassroom.Enterprise;

public class ImportCourseItemDto
{
    [ImporterHeader(Name = "课程ID")]
    public Guid CourseId { get; set; }

    [ImporterHeader(Name = "标题")]
    public string Title { get; set; }

    [ImporterHeader(Name = "排序")]
    public int Order { get; set; }

    [ImporterHeader(Name = "时长")]
    public float Duration { get; set; }

    [ImporterHeader(Name = "视频地址")]
    public string Video { get; set; }

    [ImporterHeader(Name = "开始时间")]
    public DateTime StartAt { get; set; }

    [ImporterHeader(Name = "结束时间")]
    public DateTime EndAt { get; set; }
}
