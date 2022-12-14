using DocsVision.BackOffice.ObjectModel;
using DocsVision.BackOffice.WebClient.Department;
using DocsVision.BackOffice.WebClient.Employee;
using DocsVision.Platform.WebClient;
using ServerExtension.Models;
using System;

namespace ServerExtension.Services {
    public class CustomEmployeeService : ICustomEmployeeService {
        public CustomEmployeeData GetEmployeeData(SessionContext context, Guid employeeId) {
            StaffEmployee employee = context.ObjectContext.GetObject<StaffEmployee>(employeeId);
            if (employee == null) { return null; }
            CustomEmployeeData model = new CustomEmployeeData() {
                Phone = employee.Phone
            };
            DepartmentModel departmentModel = new DepartmentModel();
            departmentModel.Initialize(employee.Unit);
            model.Unit = departmentModel;
            StaffEmployee manager = employee.Manager != null ? employee.Manager : employee.Unit.Manager;
            if (manager != null) {
                EmployeeModel managerModel = new EmployeeModel();
                managerModel.Initialize(manager);
                model.Manager = managerModel;
            }
            return model;
        }
    }
}