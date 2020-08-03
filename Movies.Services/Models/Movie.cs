using System;
using System.Collections.Generic;
using System.Text;
using Movies.Core;

namespace Movies.Services.Models
{
	public class Movie
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string PosterPath { get; set; }
		
		public string PosterUrl => string.IsNullOrWhiteSpace(PosterPath) 
			? null 
			: string.Format(Constants.Tmdb.BackdropUrlFormat, PosterPath);
	}
}
