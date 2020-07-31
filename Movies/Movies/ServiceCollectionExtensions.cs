using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Movies.Presentation.ViewModels;
using Movies.Views;
using Xamarin.Forms;

namespace Movies
{
	public static class ServiceCollectionExtensions
	{
		public static void AddViewModels(this IServiceCollection services)
		{
			services.AddAllSubclassesOf<ViewModelBase>(typeof(ViewModelBase).Assembly);
		}

		public static void AddPages(this IServiceCollection services)
		{
			services.AddAllSubclassesOf<Page>(typeof(AppShell).Assembly);
		}

		public static void AddAllSubclassesOf<T>(
			this IServiceCollection services, 
			Assembly assembly, 
			ServiceLifetime lifetime = ServiceLifetime.Transient)
		{
			var types = assembly
				.DefinedTypes
				.Where(t => t.IsSubclassOf(typeof(T)));

			foreach (var type in types)
			{
				services.AddTransient(type.AsType());
			}
		}
	}
}
