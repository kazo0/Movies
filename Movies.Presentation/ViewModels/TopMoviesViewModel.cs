using System.Threading.Tasks;
using Movies.Services;
using Movies.Services.Models;

namespace Movies.Presentation.ViewModels
{
	public class TopMoviesViewModel : PagingViewModel<Movie>
	{
		private readonly IMoviesService _moviesService;

		public TopMoviesViewModel(IMoviesService moviesService)
		{
			_moviesService = moviesService;
		}

		protected override async Task<PagedList<Movie>> GetItems(int page)
		{
			return await _moviesService.GetTopMovies(page);
		}
	}
}
