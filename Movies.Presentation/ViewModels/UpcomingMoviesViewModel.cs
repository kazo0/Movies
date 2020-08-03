using System.Threading.Tasks;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;

namespace Movies.Presentation.ViewModels
{
    public class UpcomingMoviesViewModel : MoviesPagingViewModel
    {
        public UpcomingMoviesViewModel(IMoviesService moviesService) : base(moviesService)
        {
        }
        
        protected override async Task<PagedList<Movie>> GetItems(int page)
        {
            return await MoviesService.GetUpcomingMovies(page);
        }
    }
}