using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Presentation.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TopMoviesView : ContentPage
	{
		public TopMoviesView()
		{
			InitializeComponent();
			BindingContext = Startup.ServiceProvider.GetService<TopMoviesViewModel>();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			MainThread.InvokeOnMainThreadAsync(() => ((TopMoviesViewModel) BindingContext).Init());
		}
	}
}