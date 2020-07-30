using Newtonsoft.Json;

namespace Movies.Clients.Models
{
	public class ErrorResponse
	{
		[JsonProperty("status_message")]
		public string StatusMessage { get; set; }

		[JsonProperty("status_code")]
		public int StatusCode { get; set; }
	}
}
