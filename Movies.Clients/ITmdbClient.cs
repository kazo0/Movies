using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Movies.Clients.Models;

namespace Movies.Clients
{
	public interface ITmdbClient
	{
		Task<PagedResponse<Movie>> GetTopRatedMovies(int page);
		Task<PagedResponse<Movie>> GetPopularMovies(int page);
		Task<PagedResponse<Movie>> GetNowPlayingMovies(int page);
		Task<PagedResponse<Movie>> GetUpcomingMovies(int page);
		Task<PagedResponse<Movie>> SearchMovies(string query, int page);
		Task<MovieDetail> GetMovieDetails(long movieId);
	}
}
