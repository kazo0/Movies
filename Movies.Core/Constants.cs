using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Core
{
	public class Constants
	{
		public static class Tmdb
		{
			public static readonly string TOP_RATED = "movie/top_rated";
			public static readonly string POPULAR = "movie/popular";
			public static readonly string NOW_PLAYING = "movie/now_playing";
			public static readonly string UPCOMING = "movie/upcoming";
			public static readonly string MOVIE_SEARCH = "search/movie";
			public static readonly string MOVIE_DETAILS = "movie/{0}";
		}
	}
}
