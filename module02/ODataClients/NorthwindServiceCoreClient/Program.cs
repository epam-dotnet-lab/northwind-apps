using System;
using System.Threading;

namespace NorthwindServiceCoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".NET Core console app.");

            const string serviceUri = "https://services.odata.org/V3/Northwind/Northwind.svc/";
            var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri));

            ManualResetEventSlim mre = new ManualResetEventSlim();

            IAsyncResult asyncResult = entities.Employees.BeginExecute((ar) =>
            {
                var employees = entities.Employees.EndExecute(ar);

                Console.WriteLine("Employees in Northwind service:");
                foreach (var person in employees)
                {
                    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
                }

                mre.Set();
            }, null);

            WaitHandle.WaitAny(new[] { mre.WaitHandle });

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
