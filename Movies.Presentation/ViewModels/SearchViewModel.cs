using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Movies.Core.Extensions;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace Movies.Presentation.ViewModels
{
    public class SearchViewModel : MoviesPagingViewModel
    {
	    private string _searchTerm;
        
        public ICommand Search => new AsyncCommand<string>(SearchMovies);
        
        public SearchViewModel(IMoviesService moviesService) : base(moviesService)
        {
        }

        protected override async Task<PagedList<Movie>> GetItems(int page)
        {
           return await MoviesService.SearchMovies(_searchTerm, page);
        }

        public override async Task Init()
        {
        }

        private async Task SearchMovies(string query)
        {
	        _searchTerm = query;
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            var results = await GetItems(CurrentPage);
            if (results?.Items.Any() ?? false)
            {
                CurrentPage = results.Page;
                Items = new ObservableRangeCollection<MovieItemViewModel>(results.Items.Safe().Select(ToItemViewModel));
            }
            else
            {
                ItemsThreshold = -1;
            }

            IsBusy = false;
        }
    }
}