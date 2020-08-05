using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Clients;
using Movies.Clients.Models;
using Movies.Services.Models;
using Movie = Movies.Services.Models.Movie;
using MovieDetail = Movies.Services.Models.MovieDetail;

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

			return ToMoviePagedList(response);
		}

		public async Task<PagedList<Movie>> GetPopularMovies(int page)
		{
			var response = await _tmdbClient.GetPopularMovies(page);

			return ToMoviePagedList(response);
		}
		
		public async Task<PagedList<Movie>> GetNowPlayingMovies(int page)
		{
			var response = await _tmdbClient.GetNowPlayingMovies(page);

			return ToMoviePagedList(response);
		}

		public async Task<PagedList<Movie>> GetUpcomingMovies(int page)
		{
			var response = await _tmdbClient.GetUpcomingMovies(page);

			return ToMoviePagedList(response);
		}

		public async Task<PagedList<Movie>> SearchMovies(string query, int page)
		{
			var response = await _tmdbClient.SearchMovies(query, page);

			return ToMoviePagedList(response);
		}

		public async Task<MovieDetail> GetMovieDetails(long movieId)
		{
			var response = await _tmdbClient.GetMovieDetails(movieId);

			return ToMovieDetail(response);
		}

		private PagedList<Movie> ToMoviePagedList(PagedResponse<Clients.Models.Movie> response)
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
						Id = x.Id,
						Title = x.Title,
						PosterPath = x.PosterPath,
					}).ToList()
				};
		}

		private MovieDetail ToMovieDetail(Clients.Models.MovieDetail response)
		{
			return new MovieDetail
			{
				Id = response.Id,
				ImdbId = response.ImdbId,
				Title = response.Title,
				PosterPath = response.PosterPath,
				BackdropPath = response.BackdropPath,
				Overview = response.Overview,
				ReleaseDate = response.ReleaseDate,
				Runtime = response.Runtime,
				Tagline = string.IsNullOrWhiteSpace(response.Tagline) ? null : response.Tagline,
				VoteAverage = response.VoteAverage,
			};
		}
	}
}
