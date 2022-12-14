using DocsVision.Platform.WebClient;
using DocsVision.Platform.WebClient.Helpers;
using DocsVision.Platform.WebClient.Models;
using ServerExtension.Services;
using System.Web.Mvc;

namespace ServerExtension.Controllers {
    public class GroupEmployeesDataController : Controller {
        readonly ICurrentObjectContextProvider currentObjectContextProvider;
        readonly IGroupEmployeesService groupEmployeesService;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="GroupEmployeesDataController"/>
        /// </summary>
        public GroupEmployeesDataController(ICurrentObjectContextProvider currentObjectContextProvider, IGroupEmployeesService groupEmployeesService) {
            this.currentObjectContextProvider = currentObjectContextProvider;
            this.groupEmployeesService = groupEmployeesService;
        }

        public ActionResult GetGroupEmployees(string groupName) {
            var sessionContext = this.currentObjectContextProvider.GetOrCreateApplicationPoolSessionContext();
            var response = groupEmployeesService.GetGroupEmployees(sessionContext, groupName);

            return Content(JsonHelper.SerializeToJson(CommonResponse.CreateSuccess(response)));
        }
    }
}