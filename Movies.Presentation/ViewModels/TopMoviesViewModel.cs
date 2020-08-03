using System.Threading.Tasks;
using Movies.Services;
using Movies.Services.Models;

namespace Movies.Presentation.ViewModels
{
	public class TopMoviesViewModel : MoviesPagingViewModel
	{
		public TopMoviesViewModel(IMoviesService moviesService) : base(moviesService)
		{
		}

		protected override async Task<PagedList<Movie>> GetItems(int page)
		{
			return await MoviesService.GetTopMovies(page);
		}
	}
}
