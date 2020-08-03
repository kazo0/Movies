using System.Threading.Tasks;
using Movies.Services;
using Movies.Services.Models;

namespace Movies.Presentation.ViewModels
{
	public class PopularMoviesViewModel : MoviesPagingViewModel
	{
		public PopularMoviesViewModel(IMoviesService moviesService) : base(moviesService)
		{
		}

		protected override async Task<PagedList<Movie>> GetItems(int page)
		{
			return await MoviesService.GetPopularMovies(page);
		}
	}
}
