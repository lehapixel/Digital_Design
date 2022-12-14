using DocsVision.Platform.WebClient;
using DocsVision.Platform.WebClient.Helpers;
using DocsVision.Platform.WebClient.Models;
using ServerExtension.Services;
using System;
using System.Web.Mvc;

namespace ServerExtension.Controllers {
    public class SendForAgreementDataController : Controller {
        readonly ICurrentObjectContextProvider currentObjectContextProvider;
        readonly ISendForAgreementService sendForAgreementService;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="SendForAgreementDataController"/>
        /// </summary>
        public SendForAgreementDataController(ICurrentObjectContextProvider currentObjectContextProvider, ISendForAgreementService sendForAgreementService) {
            this.currentObjectContextProvider = currentObjectContextProvider;
            this.sendForAgreementService = sendForAgreementService;
        }

        public ActionResult GetSendForAgreement(Guid cardId) {
            var sessionContext = this.currentObjectContextProvider.GetOrCreateApplicationPoolSessionContext();
            var response = sendForAgreementService.GetSendForAgreement(sessionContext, cardId);

            return Content(JsonHelper.SerializeToJson(CommonResponse.CreateSuccess(response)));
        }
    }
}