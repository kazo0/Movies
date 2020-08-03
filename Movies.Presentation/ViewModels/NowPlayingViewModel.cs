using System.Threading.Tasks;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;

namespace Movies.Presentation.ViewModels
{
    public class NowPlayingViewModel : MoviesPagingViewModel
    {
        public NowPlayingViewModel(IMoviesService moviesService) : base(moviesService)
        {
        }
        
        protected override async Task<PagedList<Movie>> GetItems(int page)
        {
            return await MoviesService.GetNowPlayingMovies(page);
        }
    }
}