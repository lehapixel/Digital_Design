using DocsVision.Platform.WebClient;
using ServerExtension.Models;
using System;

namespace ServerExtension.Services {
    public interface ICustomEmployeeService {
        /// <summary>
        /// Получение расширенной информации о сотруднике
        /// </summary>
        /// <param name="context">Контекст ВК</param>
        /// <param name="employeeId">Id сотрудника</param>
        /// <returns></returns>
        CustomEmployeeData GetEmployeeData(SessionContext context, Guid employeeId);
    }
}