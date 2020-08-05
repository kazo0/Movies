using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Presentation.ViewModels
{
	public abstract class ItemViewModel<T> : ViewModelBase where T : class
	{
		protected T Item { get; }

		protected ItemViewModel(T item)
		{
			Item = item ?? throw new ArgumentNullException(nameof(item));
		}
	}
}
