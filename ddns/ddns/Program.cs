using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Configuration;

namespace ddns
{
    class Program
    {
		private static Domain domain;
        static void Main(string[] args)
        {
			
			SetupStaticLogger();

			Log.Information("Start Rquest.");
			try
			{
				if(string.IsNullOrWhiteSpace(domain.HostName) || string.IsNullOrWhiteSpace(domain.UserName) || string.IsNullOrWhiteSpace(domain.Password))
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

            var appSettingsSection = configuration.GetSection("GoogleDomain");
            domain = appSettingsSection.Get<Domain>();
        }

		private static void RunApp()
		{

			// Do not pass any logger in via Dependency Injection, as the class will simply reference the static logger.
			var GoogleDNS = new GoogleDNS(domain);
            var result = GoogleDNS.Request();
			if (result)
				Log.Information("Success");
			else
				Log.Error("Failed");
		}
    }
}
