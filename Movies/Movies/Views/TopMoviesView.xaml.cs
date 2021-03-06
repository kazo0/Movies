﻿using System;
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
	public partial class TopMoviesView : BaseMoviesView
	{
		public TopMoviesView()
		{
			InitializeComponent();
			BindingContext = Startup.ServiceProvider.GetService<TopMoviesViewModel>();
		}
	}
}