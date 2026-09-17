namespace EmployeeCRUDAPI.Features.Employees
{
    public class EmployeeCreateRequest
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
    }
}
