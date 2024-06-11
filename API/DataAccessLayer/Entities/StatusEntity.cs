using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class StatusEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
}
