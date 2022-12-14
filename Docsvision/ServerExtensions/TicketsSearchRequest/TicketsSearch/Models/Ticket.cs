using DocsVision.Platform.StorageServer.Extensibility;
using System.Collections.Generic;

namespace TicketsSearch.Models {
    public class Data : StorageServerExtension {
        public decimal price { get; set; }
    }

    public class Ticket : StorageServerExtension {
        public IList<Data> data { get; set; }
    }
}
