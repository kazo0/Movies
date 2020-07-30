
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Services;
using Movies.Services.Models;

namespace Movies.Presentation.ViewModels
{
	public class TopMoviesViewModel : ViewModelBase
	{
		private readonly IMoviesService _moviesService;

		public List<Movie> Movies { get; set; }

		public TopMoviesViewModel(IMoviesService moviesService)
		{
			_moviesService = moviesService;
		}

		public async Task Init()
		{
			var results = await _moviesService.GetTopMovies(1);
			Movies = results?.Items.ToList() ?? new List<Movie>();
		}
	}
}
