namespace Models
{
    public class Employee
    {
        public string ProfileImage { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string? MobileNumber { get; set; }
        public string JoinDate { get; set; }
        public int ?JobTitleId {  get; set; }
        public string? JobTitleName { get; set; }
        public int ?LocationId { get; set; }
        public string? LocationName { get; set; }
        public int ?DepartmentId {  get; set; }
        public string? DepartmentName { get; set; }
        public int ?RoleDeptLocId { get; set; }
        public string? ManagerId { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
    }
}
