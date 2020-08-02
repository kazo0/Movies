using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Movies : ContentView
    {
        public Movies()
        {
            InitializeComponent();
        }
    }
}