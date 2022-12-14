using DocsVision.Platform.WebClient;
using DocsVision.Platform.WebClient.Helpers;
using DocsVision.Platform.WebClient.Models;
using ServerExtension.Services;
using System.Web.Mvc;

namespace ServerExtension.Controllers {
    public class TicketsSearchDataController : Controller {
        readonly ICurrentObjectContextProvider currentObjectContextProvider;
        readonly ITicketsSearchService ticketsSearchService;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="TicketsSearchDataController"/>
        /// </summary>
        public TicketsSearchDataController(ICurrentObjectContextProvider currentObjectContextProvider, ITicketsSearchService ticketsSearchService) {
            this.currentObjectContextProvider = currentObjectContextProvider;
            this.ticketsSearchService = ticketsSearchService;
        }

        public ActionResult GetTicketsSearch(string destination, string dateFrom, string dateTo) {
            var sessionContext = this.currentObjectContextProvider.GetOrCreateCurrentSessionContext();
            var response = ticketsSearchService.GetTicketsSearch(sessionContext, destination, dateFrom, dateTo);

            return Content(JsonHelper.SerializeToJson(CommonResponse.CreateSuccess(response)));
        }
    }
}