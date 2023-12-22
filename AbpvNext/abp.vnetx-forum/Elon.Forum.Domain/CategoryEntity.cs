using Zhaoxi.Forum.Domain.Entities;

namespace Elon.Forum.Domain.Entities;

///<summary>
/// 模块
///<summary>
public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public bool? IsLocked { get; set; } = false;

    public int SortOrder { get; set; }

    public string Image { get; set; }

    public string Pinyin { get; set; }

    public Nullable<long> ParentCategory { get; set; }

    public bool? Enabled { get; set; } = true;

    public virtual ICollection<TopicEntity> Topics { get; set; }
}