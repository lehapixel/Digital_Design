using DocsVision.Platform.WebClient;
using ServerExtension.Models;

namespace ServerExtension.Services {
    public interface ITicketsSearchService {
        /// <summary>
        /// Получение расширенной информации о городе
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="destination">Код аэропорта назначения</param>
        /// <param name="dateFrom">Дата вылета</param>
        /// <param name="dateTo">Дата прилета</param>
        /// <returns></returns>
        TicketsSearchData GetTicketsSearch(SessionContext context, string destination, string dateFrom, string dateTo);
    }
}