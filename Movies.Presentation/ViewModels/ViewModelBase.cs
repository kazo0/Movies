using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Movies.Presentation.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
