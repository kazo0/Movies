using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Movies.Clients.Models
{
	public class PagedResponse<T>
	{
		[JsonProperty("page")]
		public long Page { get; set; }

		[JsonProperty("results")]
		public IReadOnlyList<T> Results { get; set; }

		[JsonProperty("total_results")]
		public long TotalResults { get; set; }

		[JsonProperty("total_pages")]
		public long TotalPages { get; set; }
    }
}
