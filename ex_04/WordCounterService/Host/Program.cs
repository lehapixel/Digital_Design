using System;
using System.ServiceModel;

namespace Host
{
    class Host
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(WordCounterService.WordCounterService)))
            {
                host.Open();
                Console.WriteLine("Host started!");
                Console.ReadLine();
            }
        }
    }
}