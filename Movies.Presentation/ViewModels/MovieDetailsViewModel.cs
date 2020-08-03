using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;
using MvvmHelpers.Commands;

namespace Movies.Presentation.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase, IRefreshable
    {
        private long _movieId;
        private readonly IMoviesService _moviesService;
        
        public ICommand RefreshCommand => new AsyncCommand(Refresh);

        public ICommand GoToImdbCommand => new AsyncCommand(GoToImdb);
        
        public bool IsRefreshing { get; set; }

        public MovieDetail Movie { get; set; }

        public MovieDetailsViewModel(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public async Task Init(long movieId)
        {
            IsBusy = true;
         
            _movieId = movieId;
            Movie = await _moviesService.GetMovieDetails(_movieId);

            IsBusy = false;
        }

        private async Task Refresh()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            
            Movie = await _moviesService.GetMovieDetails(_movieId);

            IsBusy = false;
        }

        private Task GoToImdb()
        {
	        return Xamarin.Essentials.Browser.OpenAsync($"https://www.imdb.com/title/{Movie.ImdbId}");
        }
    }
}