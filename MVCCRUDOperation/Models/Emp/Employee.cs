namespace MVCCRUDOperation.Models.Emp
{
    public class Employee
    {
        public long Id { get; set; }
        public string? EmpName { get; set; }
        public string? Address { get; set; }
        public string? Designation { get; set; }
    }
    public class EmpolyeeViewModel
    {
        public List<Employee> Employees1 { get; set;}
        public List<Employee> Employees2 { get; set;}
    }
}
