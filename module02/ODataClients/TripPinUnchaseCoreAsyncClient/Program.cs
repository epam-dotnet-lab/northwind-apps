using System;
using System.Threading.Tasks;

namespace TripPinUnchaseCoreAsyncClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(".NET Core async console app.");

            const string serviceUri = "https://services.odata.org/TripPinRESTierService";
            var container = new Microsoft.OData.Service.Sample.TrippinInMemory.Models.Container(new Uri(serviceUri));

            Console.WriteLine("People in TripPin service:");
            var people = await container.People.ExecuteAsync();

            foreach (var person in people)
            {
                Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
