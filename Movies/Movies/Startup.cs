using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Movies.Clients;
using Movies.Presentation.ViewModels;
using Movies.Services;
using Xamarin.Essentials;

namespace Movies
{
	public static class Startup
	{
		public static IServiceProvider ServiceProvider { get; set; }

		public static void Init()
		{
			
			var host = Host.CreateDefaultBuilder()
				.ConfigureAppConfiguration(config =>
				{
					////config.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
					//using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Movies.secrets.json");
					////read in the configuration file!
					//config.AddJsonStream(stream);
					config.SetFileProvider(new EmbeddedFileProvider(typeof(App).Assembly));
					config.AddJsonFile("secrets.json", optional: true);
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
				config.BaseAddress = new Uri(ctx.Configuration["Tmdb:BaseUrl"]);
			});
			services.AddTransient<IMoviesService, TmdbMoviesService>();
			services.RegisterViewModels();
			services.RegisterPages();
		}
	}
}
