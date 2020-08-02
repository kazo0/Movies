using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Movies.Services.Models;

namespace Movies.Services
{
	public interface IMoviesService
	{
		Task<PagedList<Movie>> GetTopMovies(int page);
		Task<PagedList<Movie>> GetPopularMovies(int page);
		Task<PagedList<Movie>> GetNowPlayingMovies(int page);
		Task<PagedList<Movie>> GetUpcomingMovies(int page);
		Task<PagedList<Movie>> SearchMovies(string query, int page);
		Task<MovieDetail> GetMovieDetails(long movieId);
	}
}
