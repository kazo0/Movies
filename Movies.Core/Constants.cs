using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Core
{
	public class Constants
	{
		public static class External
		{
			public static readonly string Imdb_Url = "https://www.imdb.com/title/{0}";
		}
		public static class Tmdb
		{
			public static readonly string BackdropUrlFormat = "https://image.tmdb.org/t/p/w500{0}";
			public static readonly string PosterUrlFormat = "https://image.tmdb.org/t/p/w185{0}";
			public static class Routes
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
}
