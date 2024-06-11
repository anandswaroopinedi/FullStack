using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class RoleEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? IsDeleted { get; set; }

    public virtual ICollection<RoleDeptLocLinkEntity> RoleDeptLocLinks { get; set; } = new List<RoleDeptLocLinkEntity>();
}
