using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Presentation.ViewModels;
using Movies.Services.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseMoviesView : ContentPage
	{
		public BaseMoviesView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MainThread.InvokeOnMainThreadAsync(() => ((PagingViewModel<MovieItemViewModel, Movie>) BindingContext).Init());
		}
	}
}