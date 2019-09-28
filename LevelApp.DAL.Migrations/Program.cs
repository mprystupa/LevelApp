using System;
using System.Linq;
using System.Reflection;
using DbUp;

namespace LevelApp.DAL.Migrations
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var connectionString = args.FirstOrDefault()
                                   ?? "Server=127.0.0.1;Database=levelapp_dev;Uid=root;Pwd=root;";

            var upgrader =
                DeployChanges.To
                    .MySqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
        }
    }
}