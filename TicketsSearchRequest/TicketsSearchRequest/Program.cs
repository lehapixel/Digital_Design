using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsSearchRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            string destination = "MOW";
            string dateFrom = "30.11.2022 0:00:00";
            string dateTo = "09.12.2022 0:00:00";

            TicketsSearch.Search search = new TicketsSearch.Search();
            decimal minPrice = search.MinPriceSearch(destination, dateFrom, dateTo);
           
            Console.WriteLine(minPrice + "");
            Console.ReadKey();
        }
    }
}
