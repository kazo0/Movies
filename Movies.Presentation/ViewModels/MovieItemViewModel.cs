using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Movies.Core;
using Movies.Core.Extensions;
using Movies.Services.Models;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Movies.Presentation.ViewModels
{
	public class MovieItemViewModel : ItemViewModel<Movie>
	{

		public string PosterUrl => string.IsNullOrWhiteSpace(Item.PosterPath)
			? null
			: string.Format(Constants.Tmdb.PosterUrlFormat, Item.PosterPath);

		public string Title => Item.Title.GetValueOrDefault();

		public ICommand NavToMovieCommand => new AsyncCommand(
			() => Shell.Current.GoToAsync($"movieDetails?movieId={Item.Id}"));

		public MovieItemViewModel(Movie movie) : base(movie)
		{
		}
	}
}
