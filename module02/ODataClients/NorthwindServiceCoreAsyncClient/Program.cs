using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindServiceCoreAsyncClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(".NET Core async console app.");

            const string serviceUri = "https://services.odata.org/V3/Northwind/Northwind.svc/";
            var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri));

            var employees = await Task<IEnumerable<NorthwindModel.Employee>>.Factory.FromAsync(entities.Employees.BeginExecute(null, null), (iar) => entities.Employees.EndExecute(iar));

            Console.WriteLine("Employees in Northwind service:");
            foreach (var person in employees)
            {
                Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
