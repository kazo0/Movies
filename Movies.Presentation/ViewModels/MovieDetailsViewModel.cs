using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Movies.Core;
using Movies.Core.Extensions;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;
using MvvmHelpers.Commands;

namespace Movies.Presentation.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase, IRefreshable
    {
        private long _movieId;
        private MovieDetail _movie;
        private readonly IMoviesService _moviesService;
        
        public ICommand RefreshCommand => new AsyncCommand(Refresh);

        public ICommand GoToImdbCommand => new AsyncCommand(GoToImdb);
        
        public bool IsRefreshing { get; set; }

        public string Title { get; set; }

        public string Overview { get; set; }

        public DateTimeOffset? ReleaseDate { get; set; }
        
        public int Runtime { get; set; }

        public string Tagline { get; set; }

        public string BackdropUrl { get; set; }

        public string PosterUrl { get; set; }

        public MovieDetailsViewModel(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public async Task Init(long movieId)
        {
            IsBusy = true;
         
            _movieId = movieId;
            _movie = await _moviesService.GetMovieDetails(_movieId);

            if (_movie != null)
            {
	            SetMovie();
            }

            IsBusy = false;
        }

        private async Task Refresh()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            
            _movie = await _moviesService.GetMovieDetails(_movieId);

            if (_movie != null)
            {
	            SetMovie();
            }

            IsBusy = false;
        }

        private Task GoToImdb()
        {
	        return Xamarin.Essentials.Browser.OpenAsync(string.Format(Constants.External.Imdb_Url, _movie.ImdbId));
        }

        private void SetMovie()
        {
            //this is crap and would be why I would want to use ReactiveUI
	        Title = _movie.Title.GetValueOrDefault();
            Overview = _movie?.Overview.GetValueOrDefault();
            ReleaseDate = _movie?.ReleaseDate.GetValueOrDefault();
            Runtime = _movie?.Runtime ?? 0;
            Tagline = _movie?.Tagline.GetValueOrDefault();
            BackdropUrl = string.IsNullOrWhiteSpace(_movie?.BackdropPath)
	            ? null
	            : string.Format(Constants.Tmdb.BackdropUrlFormat, _movie.BackdropPath);
            PosterUrl = string.IsNullOrWhiteSpace(_movie?.PosterPath)
	            ? null
	            : string.Format(Constants.Tmdb.PosterUrlFormat, _movie.PosterPath);
        }
    }
}