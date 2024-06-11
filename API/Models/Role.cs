using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Models
{
    public class Role
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName {  get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string? Description { get; set; }
        public int? RoleDeptLocId {  get; set; }
    }

}
