using System;

namespace NorthwindServiceFrameworkClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".NET Framework console app.");

            const string serviceUri = "https://services.odata.org/V3/Northwind/Northwind.svc/";
            var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri));

            var employees = entities.Employees;
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
