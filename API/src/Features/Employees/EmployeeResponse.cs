
namespace EmployeeCRUDAPI.Features.Employees
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
    }
}
