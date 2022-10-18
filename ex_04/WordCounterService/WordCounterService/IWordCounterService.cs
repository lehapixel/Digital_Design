using System.ServiceModel;
using System.Collections.Concurrent;

namespace WordCounterService
{
    [ServiceContract]
    public interface IWordCounterService
    {
        [OperationContract]
        ConcurrentDictionary<string, int> Count(string[] text);
    }
}