using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Movies.Clients;
using Movies.Presentation.ViewModels;
using Xamarin.Essentials;

namespace Movies
{
	public static class Startup
	{
		public static IServiceProvider ServiceProvider { get; set; }

		public static void Init()
		{

			using var stream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Movies.appsettings.json");
			var host = new HostBuilder()
				.ConfigureHostConfiguration(c =>
				{
					c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
					
					c.AddJsonStream(stream);
				})
				.ConfigureServices(ConfigureServices)
				.ConfigureLogging(l => l.AddConsole())
				.Build();

			ServiceProvider = host.Services;
		}

		private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
		{
			services.AddHttpClient<ITmdbClient, TmdbClient>(config =>
			{
				config.BaseAddress = new Uri(Constants.Tmdb.BASE_URL);
			});

			services.RegisterViewModels();
			services.RegisterPages();
		}
	}
}
