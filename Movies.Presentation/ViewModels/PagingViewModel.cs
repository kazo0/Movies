using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Movies.Core.Extensions;
using Movies.Presentation.Contracts;
using Movies.Services.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace Movies.Presentation.ViewModels
{
	public abstract class PagingViewModel<T> : ViewModelBase, IRefreshable
	{
		private int _maxPages = 0;

		protected virtual int CurrentPage { get; set; } = 1;

		public ObservableRangeCollection<T> Items { get; set; }
		public virtual int ItemsThreshold { get; set; } = 5;
		public ICommand NextPageCommand => new AsyncCommand(LoadNextPage);
		public ICommand RefreshCommand => new AsyncCommand(Refresh);
		public bool IsRefreshing { get; set; }

		public virtual async Task Init()
		{
			IsBusy = true;

			var results = await GetItems(CurrentPage);
			_maxPages = results.TotalPages;

			Items = new ObservableRangeCollection<T>(results?.Items.Safe() ?? new List<T>());
			IsBusy = false;
		}

		protected abstract Task<PagedList<T>> GetItems(int page);

		private async Task LoadNextPage()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			var results = await GetItems(CurrentPage + 1);
			if (results?.Items.Any() ?? false)
			{
				CurrentPage = results.Page;
				Items.AddRange(results.Items);
			}
			else
			{
				ItemsThreshold = -1;
			}

			IsBusy = false;
		}

		private async Task Refresh()
		{
			CurrentPage = 1;
			var results = await GetItems(CurrentPage);

			Items.Clear();
			Items = new ObservableRangeCollection<T>(results?.Items.Safe() ?? new List<T>());

			IsRefreshing = false;
		}
	}
}