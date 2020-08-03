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
    [QueryProperty("MovieId", "movieId")]
    public partial class MovieDetailsView : ContentPage
    {
        private string _movieId = string.Empty;
        public string MovieId
        {
            get => _movieId;
            set => _movieId = Uri.EscapeDataString(value ?? string.Empty);
        }

        public MovieDetailsView()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<MovieDetailsViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MainThread.InvokeOnMainThreadAsync(() =>
                ((MovieDetailsViewModel) BindingContext).Init(long.Parse(MovieId)));
        }
    }
}