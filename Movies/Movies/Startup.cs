﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Movies.Presentation.ViewModels;

namespace Movies
{
	public static class Startup
	{
		public static IServiceProvider ServiceProvider { get; set; }

		public static void Init()
		{
			var host = new HostBuilder()
				.ConfigureServices(ConfigureServices)
				.ConfigureLogging(l => l.AddConsole())
				.Build();

			ServiceProvider = host.Services;
		}

		private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
		{
			services.AddHttpClient();
			services.RegisterViewModels();
			services.RegisterPages();
		}
	}
}
