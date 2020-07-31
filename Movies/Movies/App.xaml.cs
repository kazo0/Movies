using System;
using Movies.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			Startup.Init();
			MainPage = Startup.ServiceProvider.GetService<AppShell>();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
