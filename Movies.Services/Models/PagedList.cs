using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Services.Models
{
	public class PagedList<T>
	{
		public int Total { get; set; }
		public int Page { get; set; }
		public IReadOnlyList<T> Items { get; set; }
	}
}
