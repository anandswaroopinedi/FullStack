using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class EmployeeEntity
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string? MobileNumber { get; set; }

    public string JoinDate { get; set; } = null!;

    public string ProfileImage { get; set; } = null!;

    public string? ManagerId { get; set; }

    public int? RoleDeptLocId { get; set; }

    public int ProjectId { get; set; }

    public int StatusId { get; set; }

    public virtual ICollection<EmployeeEntity> InverseManager { get; set; } = new List<EmployeeEntity>();

    public virtual EmployeeEntity? Manager { get; set; }

    public virtual ProjectEntity Project { get; set; } = null!;

    public virtual RoleDeptLocLinkEntity? RoleDeptLoc { get; set; } 

    public virtual StatusEntity Status { get; set; } = null!;
}
