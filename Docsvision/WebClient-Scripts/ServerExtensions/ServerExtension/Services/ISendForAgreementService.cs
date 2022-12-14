using DocsVision.Platform.WebClient;
using ServerExtension.Models;
using System;

namespace ServerExtension.Services {
    public interface ISendForAgreementService {
        /// <summary>
        /// Получение расширенной информации о городе
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="cardId">Id карточки</param>
        /// <returns></returns>
        SendForAgreementData GetSendForAgreement(SessionContext context, Guid cardId);
    }
}