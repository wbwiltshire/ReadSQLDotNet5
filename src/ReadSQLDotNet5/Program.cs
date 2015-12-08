using System;
using System.Threading.Tasks;

namespace ReadSQLDotNet5
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Test synchronous access to the database
            Console.WriteLine("Starting sychronous DB access.");
            SyncDB sdb = new SyncDB();

            sdb.FindAll();
            Console.WriteLine();
            sdb.FindByPK(4);
            Console.WriteLine();
            sdb.Close();

            Console.WriteLine("Sychronous DB access complete.");
            Console.WriteLine();

            //Test Asynchronous access to the database
            Console.WriteLine("Starting asychronous DB access.");
            AsyncDB adb = new AsyncDB();

            var t = new TaskFactory().StartNew(async () =>
            {
                await adb.FindAll();
                Console.WriteLine();
                await adb.FindByPK(4);
                Console.WriteLine();
                adb.Close();

                Console.WriteLine();
                Console.WriteLine("Asychronous DB access complete.");
            });

            Console.WriteLine();
            Console.WriteLine("Press <enter> key to when you see that Asyncychronous DB access is complete.");
            Console.ReadLine();
        }
    }
}
