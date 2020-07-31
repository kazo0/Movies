
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Movies.Core.Extensions;
using Movies.Services;
using Movies.Services.Models;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Movies.Presentation.ViewModels
{
	public class TopMoviesViewModel : ViewModelBase
	{
		private readonly IMoviesService _moviesService;
		private int _currentPage = 1;

		public ObservableRangeCollection<Movie> Movies { get; set; }

		public ICommand NextPageCommand => new Command(async () => await LoadNextPage());

		public TopMoviesViewModel(IMoviesService moviesService)
		{
			_moviesService = moviesService;
		}

		public async Task Init()
		{
			var results = await _moviesService.GetTopMovies(_currentPage);
			MainThread.BeginInvokeOnMainThread(() =>
			{
				Movies = new ObservableRangeCollection<Movie>(results?.Items.Safe() ?? new List<Movie>());
			});
		}

		private async Task LoadNextPage()
		{
			var results = await _moviesService.GetTopMovies(++_currentPage);
			if (results?.Items.Any() ?? false)
			{
				MainThread.BeginInvokeOnMainThread(() => { Movies.AddRange(results.Items); });
			}
		}
	}
}
