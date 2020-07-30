﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Movies.Clients.Models;

namespace Movies.Clients
{
	public interface ITmdbClient
	{
		Task<PagedResponse<Movie>> GetTopRatedMovies(int page);
	}
}