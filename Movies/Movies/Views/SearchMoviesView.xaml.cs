using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Presentation.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchMoviesView : ContentPage
    {
        public SearchMoviesView()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<SearchViewModel>();
        }
    }
}