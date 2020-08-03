using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Movies.Presentation.Contracts;
using Movies.Services;
using Movies.Services.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace Movies.Presentation.ViewModels
{
    public class SearchViewModel : MoviesPagingViewModel
    {
        public string SearchTerm { get; set; }
        
        public ICommand Search => new AsyncCommand(SearchMovies);
        
        public SearchViewModel(IMoviesService moviesService) : base(moviesService)
        {
        }

        protected override async Task<PagedList<Movie>> GetItems(int page)
        {
           return await MoviesService.SearchMovies(SearchTerm, page);
        }

        private async Task SearchMovies()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            var results = await GetItems(CurrentPage);
            if (results?.Items.Any() ?? false)
            {
                CurrentPage = results.Page;
                Items = new ObservableRangeCollection<Movie>(results.Items);
            }
            else
            {
                ItemsThreshold = -1;
            }

            IsBusy = false;
        }
    }
}