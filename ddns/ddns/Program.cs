using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ddns
{
    class Program
    {
		private static List<Domain> domain = new List<Domain>();
        static void Main(string[] args)
        {
			
			SetupStaticLogger();

			Log.Information("Start Rquest.");
			try
			{

				if(domain.Count < 0 || string.IsNullOrWhiteSpace(domain[0].HostName) || string.IsNullOrWhiteSpace(domain[0].UserName) || string.IsNullOrWhiteSpace(domain[0].Password))
                {
					Log.Error("Appsettings invalid");
					return;
                }			    
				RunApp();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "An unhandled exception occurred.");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}
        private static void SetupStaticLogger()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();
	

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
			configuration.GetSection("GoogleDomain").Bind(domain);
			
        }

		private static void RunApp()
		{

			// Do not pass any logger in via Dependency Injection, as the class will simply reference the static logger.
			domain.ForEach(x =>
			{
				var GoogleDNS = new GoogleDNS(x);
				var result = GoogleDNS.Request();
				if (result)
					Log.Information("Success");
				else
					Log.Error("Failed");

			});
			
		}
    }
}
