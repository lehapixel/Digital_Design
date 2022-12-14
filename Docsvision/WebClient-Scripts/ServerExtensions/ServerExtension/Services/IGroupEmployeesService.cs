using DocsVision.Platform.WebClient;
using ServerExtension.Models;

namespace ServerExtension.Services {
    public interface IGroupEmployeesService {
        /// <summary>
        /// Получение расширенной информации о сотруднике
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="groupName">Название группы</param>
        /// <returns></returns>
        GroupEmployeesData GetGroupEmployees(SessionContext context, string groupName);
    }
}