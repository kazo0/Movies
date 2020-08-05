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
	public abstract class PagingViewModel<TItemViewModel, TItem> : ViewModelBase, IRefreshable 
		where TItemViewModel : ItemViewModel<TItem>
		where TItem : class
	{
		private int _maxPages = 0;

		protected virtual int CurrentPage { get; set; } = 1;

		public ObservableRangeCollection<TItemViewModel> Items { get; set; }
		public virtual int ItemsThreshold { get; set; } = 5;
		public ICommand NextPageCommand => new AsyncCommand(LoadNextPage);
		public ICommand RefreshCommand => new AsyncCommand(Refresh);
		public bool IsRefreshing { get; set; }

		public virtual async Task Init()
		{
			IsBusy = true;

			var results = await GetItems(CurrentPage);
			_maxPages = results.TotalPages;

			Items = new ObservableRangeCollection<TItemViewModel>(results?.Items.Safe().Select(ToItemViewModel));
			IsBusy = false;
		}

		protected abstract Task<PagedList<TItem>> GetItems(int page);
		protected abstract TItemViewModel ToItemViewModel(TItem item);

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
				Items.AddRange(results.Items.Safe().Select(ToItemViewModel));
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
			Items = new ObservableRangeCollection<TItemViewModel>(results?.Items.Safe().Select(ToItemViewModel));

			IsRefreshing = false;
		}
	}
}