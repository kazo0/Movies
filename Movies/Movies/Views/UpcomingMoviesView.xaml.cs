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
    public partial class UpcomingMoviesView : ContentPage
    {
        public UpcomingMoviesView()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<UpcomingMoviesViewModel>();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MainThread.InvokeOnMainThreadAsync(() => ((UpcomingMoviesViewModel) BindingContext).Init());
        }
    }
}