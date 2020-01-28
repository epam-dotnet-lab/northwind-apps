using System;
using System.Linq;
using System.Threading;

namespace TripPinUnchaseCoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".NET Core console app.");

            const string serviceUri = "https://services.odata.org/TripPinRESTierService";
            var container = new Microsoft.OData.Service.Sample.TrippinInMemory.Models.Container(new Uri(serviceUri));

            IAsyncResult asyncResult = container.People.BeginExecute((ar) =>
            {
                Console.WriteLine("People in TripPin service:");
                var people = container.People.EndExecute(ar).ToArray();

                foreach (var person in people)
                {
                    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
                }

            }, null);

            WaitHandle.WaitAny(new[] { asyncResult.AsyncWaitHandle });

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
