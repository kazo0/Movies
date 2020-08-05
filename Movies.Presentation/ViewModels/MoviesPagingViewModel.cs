using System.Windows.Input;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Movies.Presentation.ViewModels
{
    public abstract class MoviesPagingViewModel : PagingViewModel<MovieItemViewModel, Movie>
    {
        protected readonly IMoviesService MoviesService;
        
        protected MoviesPagingViewModel(IMoviesService moviesService)
        {
            MoviesService = moviesService;
        }

        protected override MovieItemViewModel ToItemViewModel(Movie item)
        {
	        return new MovieItemViewModel(item);
        }
    }
}