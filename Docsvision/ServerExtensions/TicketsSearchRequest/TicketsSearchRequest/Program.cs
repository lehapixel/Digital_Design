using System;

namespace TicketSearchRequest {
    class Program {
        static void Main(string[] args) {
            string destination = "KUF";
            string dateFrom = "17.12.2022";
            string dateTo = "20.12.2022";

            TicketsSearch.Search search = new TicketsSearch.Search();
            decimal minPrice = search.MinPriceSearch(destination, dateFrom, dateTo);
           
            Console.WriteLine(minPrice + "");
            Console.ReadKey();
        }
    }
}
