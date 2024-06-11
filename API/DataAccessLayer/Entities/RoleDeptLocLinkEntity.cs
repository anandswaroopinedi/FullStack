using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class RoleDeptLocLinkEntity
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int DepartmentId { get; set; }

    public int LocationId { get; set; }

    public string? Description { get; set; }

    public int? IsDeleted { get; set; }

    public virtual DepartmentEntity Department { get; set; } = null!;

    public virtual ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();

    public virtual LocationEntity Location { get; set; } = null!;

    public virtual RoleEntity Role { get; set; } = null!;
}
