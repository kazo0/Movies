using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Clients;
using Movies.Clients.Models;
using Movies.Services.Models;
using Movie = Movies.Services.Models.Movie;

namespace Movies.Services
{
	public class TmdbMoviesService : IMoviesService
	{
		private readonly ITmdbClient _tmdbClient;

		public TmdbMoviesService(ITmdbClient tmdbClient)
		{
			_tmdbClient = tmdbClient;
		}

		public async Task<PagedList<Movie>> GetTopMovies(int page)
		{
			var response = await _tmdbClient.GetTopRatedMovies(page);

			return ToMoviePageList(response);
		}

		public async Task<PagedList<Movie>> GetPopularMovies(int page)
		{
			var response = await _tmdbClient.GetPopularMovies(page);

			return ToMoviePageList(response);
		}
		
		public async Task<PagedList<Movie>> GetNowPlayingMovies(int page)
		{
			var response = await _tmdbClient.GetNowPlayingMovies(page);

			return ToMoviePageList(response);
		}

		private PagedList<Movie> ToMoviePageList(PagedResponse<Clients.Models.Movie> response)
		{
			return response == null
				? null
				: new PagedList<Movie>
				{
					Total = response.TotalResults,
					TotalPages = response.TotalPages,
					Page = response.Page,
					Items = response.Results.Select(x => new Movie
					{
						Title = x.Title,
						PosterUrl = $"https://image.tmdb.org/t/p/w185{x.PosterPath}"
					}).ToList()
				};
		}
	}
}
