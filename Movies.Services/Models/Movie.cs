﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Services.Models
{
	public class Movie
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string PosterPath { get; set; }
	}
}
