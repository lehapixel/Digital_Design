using DocsVision.BackOffice.ObjectModel;
using DocsVision.BackOffice.ObjectModel.Services;
using DocsVision.BackOffice.WebClient.Employee;
using DocsVision.Platform.WebClient;
using ServerExtension.Models;
using System.Collections.Generic;

namespace ServerExtension.Services {
    public class GroupEmployeesService : IGroupEmployeesService {
        public GroupEmployeesData GetGroupEmployees(SessionContext context, string groupName) {
            IStaffService staffService = context.ObjectContext.GetService<IStaffService>();

            StaffGroup groupId = staffService.FindGroupByName(null, groupName);
           
            List<EmployeeModel> employeesList = new List<EmployeeModel>();
            foreach (StaffEmployee employee in groupId.Employees) {
                EmployeeModel employeesModel = new EmployeeModel();
                employeesModel.Initialize(employee);
                employeesList.Add(employeesModel);
            }

            GroupEmployeesData model = new GroupEmployeesData(); {
                model.Employees = employeesList.ToArray();
            };
            return model;
        }
    }
}