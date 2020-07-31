
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Movies.Core.Extensions;
using Movies.Services;
using Movies.Services.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Essentials;

namespace Movies.Presentation.ViewModels
{
	public class TopMoviesViewModel : ViewModelBase
	{
		private readonly IMoviesService _moviesService;
		private int _currentPage = 1;
		private int _maxPages = 0;

		public ObservableRangeCollection<Movie> Movies { get; set; }
		public int MoviesThreshold { get; set; } = 5;

		public bool IsRefreshing { get; set; }

		public ICommand NextPageCommand => new AsyncCommand(LoadNextPage);
		public ICommand RefreshCommand => new AsyncCommand(Refresh);

		public TopMoviesViewModel(IMoviesService moviesService)
		{
			_moviesService = moviesService;
		}

		public async Task Init()
		{
			IsBusy = true;

			var results = await _moviesService.GetTopMovies(_currentPage);
			_maxPages = results.TotalPages;

			Movies = new ObservableRangeCollection<Movie>(results?.Items.Safe() ?? new List<Movie>());
			IsBusy = false;
		}

		private async Task LoadNextPage()
		{
			if (IsBusy)
			{
				return;
			}
			IsBusy = true;

			var results = await _moviesService.GetTopMovies(_currentPage + 1);
			if (results?.Items.Any() ?? false)
			{
				_currentPage = results.Page;
				Movies.AddRange(results.Items);
			}
			else
			{
				MoviesThreshold = -1;
			}

			IsBusy = false;
		}

		private async Task Refresh()
		{
			_currentPage = 1;
			var results = await _moviesService.GetTopMovies(_currentPage);

			Movies.Clear();
			Movies = new ObservableRangeCollection<Movie>(results?.Items.Safe() ?? new List<Movie>());

			IsRefreshing = false;
		}
	}
}
