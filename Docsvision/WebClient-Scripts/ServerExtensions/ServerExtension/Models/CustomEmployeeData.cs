using DocsVision.BackOffice.WebClient.Department;
using DocsVision.BackOffice.WebClient.Employee;

namespace ServerExtension.Models {
    public class CustomEmployeeData {
        public string Phone { get; set; }
        public EmployeeModel Manager { get; set; }
        public DepartmentModel Unit { get; set; }
    }
}