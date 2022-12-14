using DocsVision.BackOffice.ObjectModel;
using DocsVision.Platform.WebClient;
using ServerExtension.Models;
using System;

namespace ServerExtension.Services {
    public class CityInfoService : ICityInfoService {
        public CityInfoData GetCityInfo(SessionContext context, Guid cityId) {
            BaseUniversalItem cityItem = context.ObjectContext.GetObject<BaseUniversalItem>(cityId);
            if (cityItem == null) { return null; }
            CityInfoData model = new CityInfoData() {
                AirportСode = Convert.ToString(cityItem.ItemCard.MainInfo["AirportСode"]),
                PayPerDay = Convert.ToDecimal(cityItem.ItemCard.MainInfo["PayPerDay"])
            };
            return model;
        }
    }
}