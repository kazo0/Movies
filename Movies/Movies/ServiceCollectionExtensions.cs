using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Movies.Presentation.ViewModels;
using Xamarin.Forms;

namespace Movies
{
	public static class ServiceCollectionExtensions
	{
		public static void RegisterViewModels(this IServiceCollection services)
		{
			services.RegisterAllSubclassesOf<ViewModelBase>(typeof(ViewModelBase).Assembly);
		}

		public static void RegisterPages(this IServiceCollection services)
		{
			services.RegisterAllSubclassesOf<Page>(typeof(MainPage).Assembly);
		}

		public static void RegisterAllSubclassesOf<T>(
			this IServiceCollection services, 
			Assembly assembly, 
			ServiceLifetime lifetime = ServiceLifetime.Transient)
		{
			var types = assembly
				.DefinedTypes
				.Where(t => t.IsSubclassOf(typeof(T)));

			foreach (var type in types)
			{
				services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
			}
		}
	}
}
