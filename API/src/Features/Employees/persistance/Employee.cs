using EmployeeCRUDAPI.Features.Departments.persistance;

namespace EmployeeCRUDAPI.Features.Employees.persistance
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
