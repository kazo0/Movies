using System;

namespace Movies.Services.Models
{
    public class MovieDetail : Movie
    {
        public string BackdropPath { get; set; }
        public string ImdbId { get; set; }
        public string Overview { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public string Tagline { get; set; }
        public double VoteAverage { get; set; }
    }
}