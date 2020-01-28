using System;

namespace TripPinUnchaseFrameworkClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".NET Framework console app.");

            const string serviceUri = "https://services.odata.org/TripPinRESTierService";
            var container = new Microsoft.OData.Service.Sample.TrippinInMemory.Models.Container(new Uri(serviceUri));

            var people = container.People;

            Console.WriteLine("People in TripPin service:");

            foreach (var person in people)
            {
                Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
