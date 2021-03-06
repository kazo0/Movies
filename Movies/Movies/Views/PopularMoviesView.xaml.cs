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
	public partial class PopularMoviesView : BaseMoviesView
	{
		public PopularMoviesView()
		{
			InitializeComponent();
			BindingContext = Startup.ServiceProvider.GetService<PopularMoviesViewModel>();
		}
	}
}