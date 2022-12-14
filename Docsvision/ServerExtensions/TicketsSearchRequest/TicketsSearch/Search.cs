using DocsVision.Platform.StorageServer.Extensibility;
using Newtonsoft.Json;
using TicketsSearch.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;


namespace TicketsSearch {
    /// <summary>
    /// Класс серверного расширения
    /// </summary>
    public class Search : StorageServerExtension {
        [ExtensionMethod]
        public decimal MinPriceSearch(string destination, string dateFrom, string dateTo) {
            string departure_at = DateTime.Parse(dateFrom).ToString("yyyy-MM-dd");
            string return_at = DateTime.Parse(dateTo).ToString("yyyy-MM-dd");
            decimal resultPrice;

            string url = $"https://api.travelpayouts.com/aviasales/v3/prices_for_dates?origin=LED&destination={ destination }&currency=rub&departure_at={ departure_at }&return_at={ return_at }&sorting=price&direct=true&limit=1000&token=5cc00f551ed873849a5911e970d5bf42";
            try {
                try {
                    string result = GetContent(url);

                    var ticketsData = JsonConvert.DeserializeObject<Ticket>(result);

                    var minPrice = ticketsData.data
                        .Select(p => p.price)
                        .Min();
                    resultPrice = minPrice;
                }
                catch (InvalidOperationException) {
                    resultPrice = 0;
                }
            }
            catch (WebException)
            {
                resultPrice = 0;
            }

            return resultPrice;
        }

        [ExtensionMethod]
        public static string GetContent(string url) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.Accept = "application/json";
            request.UserAgent = "Mozilla/5.0 ....";

            request.Proxy = WebRequest.DefaultWebProxy;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            response.Close();
            return output.ToString();
        }
    }
}