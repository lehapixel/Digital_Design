using DocsVision.Platform.WebClient;
using DocsVision.Platform.WebClient.Helpers;
using DocsVision.Platform.WebClient.Models;
using ServerExtension.Services;
using System;
using System.Web.Mvc;

namespace ServerExtension.Controllers {
    public class CityInfoDataController : Controller {
        readonly ICurrentObjectContextProvider currentObjectContextProvider;
        readonly ICityInfoService cityInfoService;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="CityInfoDataController"/>
        /// </summary>
        public CityInfoDataController(ICurrentObjectContextProvider currentObjectContextProvider, ICityInfoService cityInfoService) {
            this.currentObjectContextProvider = currentObjectContextProvider;
            this.cityInfoService = cityInfoService;
        }

        public ActionResult GetCityInfo(Guid cityId) {
            var sessionContext = this.currentObjectContextProvider.GetOrCreateApplicationPoolSessionContext();
            var response = cityInfoService.GetCityInfo(sessionContext, cityId);

            return Content(JsonHelper.SerializeToJson(CommonResponse.CreateSuccess(response)));
        }
    }
}