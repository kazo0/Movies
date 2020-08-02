using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Movies.Clients.Models;
using Movies.Core;
using Newtonsoft.Json;

namespace Movies.Clients
{
	public class TmdbClient : ITmdbClient
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<TmdbClient> _logger;
		private readonly IConfiguration _configuration;

		public TmdbClient(HttpClient httpClient, ILogger<TmdbClient> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
			_httpClient = httpClient;
		}

		public async Task<PagedResponse<Movie>> GetTopRatedMovies(int page)
		{
			return await GetResponse<PagedResponse<Movie>>(
				Constants.Tmdb.TOP_RATED
					.SetQueryParam("page", page));
		}

		public async Task<PagedResponse<Movie>> GetPopularMovies(int page)
		{
			return await GetResponse<PagedResponse<Movie>>(
				Constants.Tmdb.POPULAR
					.SetQueryParam("page", page));
		}
		
		public async Task<PagedResponse<Movie>> GetNowPlayingMovies(int page)
		{
			return await GetResponse<PagedResponse<Movie>>(
				Constants.Tmdb.NOW_PLAYING
					.SetQueryParam("page", page));
		}

		public async Task<PagedResponse<Movie>> GetUpcomingMovies(int page)
		{
			return await GetResponse<PagedResponse<Movie>>(
				Constants.Tmdb.UPCOMING
					.SetQueryParam("page", page));
		}

		public async Task<PagedResponse<Movie>> SearchMovies(string query, int page)
		{
			return await GetResponse<PagedResponse<Movie>>(
				Constants.Tmdb.MOVIE_SEARCH
					.SetQueryParam("query", query)
					.SetQueryParam("page", page)
					.SetQueryParam("include_adult", false));
		}

		public async Task<MovieDetail> GetMovieDetails(long movieId)
		{
			return await GetResponse<MovieDetail>(
				string.Format(Constants.Tmdb.MOVIE_SEARCH, movieId));
		}

		private async Task<T> GetResponse<T>(string requestUri)
		{
			var response = await _httpClient.GetAsync(requestUri
				.SetQueryParam("api_key", _configuration["TmdbApiKey"])
				.SetQueryParam("language", CultureInfo.CurrentCulture.TwoLetterISOLanguageName));


			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<T>(json);
			}

			var errorJson = await response.Content.ReadAsStringAsync();
			var error = JsonConvert.DeserializeObject<ErrorResponse>(errorJson);

			_logger.LogError($"An error occurred fetching Top Rated Movies: {error.StatusMessage}");

			return default;
		}
	}
}
