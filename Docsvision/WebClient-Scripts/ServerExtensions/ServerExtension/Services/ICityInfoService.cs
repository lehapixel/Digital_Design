using DocsVision.Platform.WebClient;
using ServerExtension.Models;
using System;

namespace ServerExtension.Services {
    public interface ICityInfoService {
        /// <summary>
        /// Получение расширенной информации о городе
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="cityId">Id города</param>
        /// <returns></returns>
        CityInfoData GetCityInfo(SessionContext context, Guid cityId);
    }
}