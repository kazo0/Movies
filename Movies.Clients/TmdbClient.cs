using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Movies.Clients.Models;
using Newtonsoft.Json;

namespace Movies.Clients
{
	public class TmdbClient : ITmdbClient
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<TmdbClient> _logger;

		public TmdbClient(HttpClient httpClient, ILogger<TmdbClient> logger)
		{
			_logger = logger;
			_httpClient = httpClient;
		}

		public async Task<PagedResponse<Movie>> GetTopRatedMovies(int page)
		{
			return await GetResponse<PagedResponse<Movie>>(
				Constants.Tmdb.TOP_RATED
					.SetQueryParam("page", page));
		}

		private async Task<T> GetResponse<T>(string requestUri)
		{
			var response = await _httpClient.GetAsync(
				requestUri
					.SetQueryParam("api_key", "key")
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
