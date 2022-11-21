using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsSearch.Models
{
    public class Data
    {
        public decimal price { get; set; }
    }

    public class Ticket
    {
        public IList<Data> data { get; set; }
    }
}
