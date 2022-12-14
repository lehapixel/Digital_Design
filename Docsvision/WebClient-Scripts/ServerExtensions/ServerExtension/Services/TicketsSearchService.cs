using DocsVision.Platform.ObjectManager;
using DocsVision.Platform.WebClient;
using ServerExtension.Models;
using System;

namespace ServerExtension.Services {
    public class TicketsSearchService : ITicketsSearchService {
        public TicketsSearchData GetTicketsSearch(SessionContext context, string destination, string dateFrom, string dateTo) {
            ExtensionMethod method = context.Session.ExtensionManager.GetExtensionMethod("TicketsSearch", "MinPriceSearch");
            method.Parameters.AddNew("destination", ParameterValueType.String, destination);
            method.Parameters.AddNew("dateFrom", ParameterValueType.String, dateFrom);
            method.Parameters.AddNew("dateTo", ParameterValueType.String, dateTo);

           TicketsSearchData model = new TicketsSearchData() {
                TicketsPrice = Convert.ToString(method.Execute())
            };
            return model;
        }
    }
}