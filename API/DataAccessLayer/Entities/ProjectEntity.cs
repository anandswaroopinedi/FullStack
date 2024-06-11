using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class ProjectEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? IsDeleted { get; set; }

    public virtual ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
}
