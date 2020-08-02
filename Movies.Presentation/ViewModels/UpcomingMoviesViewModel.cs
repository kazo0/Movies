using System.Threading.Tasks;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;

namespace Movies.Presentation.ViewModels
{
    public class UpcomingMoviesViewModel : PagingViewModel<Movie>, IRefreshable
    {
        private readonly IMoviesService _moviesService;

        public UpcomingMoviesViewModel(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }
        
        protected override async Task<PagedList<Movie>> GetItems(int page)
        {
            return await _moviesService.GetUpcomingMovies(page);
        }
    }
}