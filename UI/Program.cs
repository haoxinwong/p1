using System;
using Serilog;

namespace P0_M.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("../logs/logs.txt",rollingInterval:RollingInterval.Day)
            .CreateLogger();

            Log.Information("Application Starting..");
            new MainMenu().Start();
        }
    }
}