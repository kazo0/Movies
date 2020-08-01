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
	}
}
