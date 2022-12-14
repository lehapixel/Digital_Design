using DocsVision.Platform.WebClient;
using DocsVision.Platform.WebClient.Helpers;
using DocsVision.Platform.WebClient.Models;
using ServerExtension.Services;
using System;
using System.Web.Mvc;

namespace ServerExtension.Controllers {
    public class CustomEmployeeDataController : Controller {
        readonly ICurrentObjectContextProvider currentObjectContextProvider;
        readonly ICustomEmployeeService customEmployeeService;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="CustomEmployeeDataController"/>
        /// </summary>
        public CustomEmployeeDataController(ICurrentObjectContextProvider currentObjectContextProvider, ICustomEmployeeService customEmployeeService) {
            this.currentObjectContextProvider = currentObjectContextProvider;
            this.customEmployeeService = customEmployeeService;
        }

        public ActionResult GetEmployeeData(Guid employeeId) {
            var sessionContext = this.currentObjectContextProvider.GetOrCreateApplicationPoolSessionContext();
            var response = customEmployeeService.GetEmployeeData(sessionContext, employeeId);
            
            return Content(JsonHelper.SerializeToJson(CommonResponse.CreateSuccess(response)));
        }
    }
}