using System.Windows.Input;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Movies.Presentation.ViewModels
{
    public abstract class MoviesPagingViewModel : PagingViewModel<Movie>, IRefreshable
    {
        protected readonly IMoviesService MoviesService;

        public ICommand NavToMovieCommand => new AsyncCommand<Movie>(
            (movie) => Shell.Current.GoToAsync($"movieDetails?movieId={movie.Id}"));
        
        protected MoviesPagingViewModel(IMoviesService moviesService)
        {
            MoviesService = moviesService;
        }
    }
}