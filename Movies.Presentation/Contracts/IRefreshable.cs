using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Movies.Presentation.Contracts
{
	public interface IRefreshable
	{
		ICommand RefreshCommand { get; }
		bool IsRefreshing { get; set; }
	}
}
