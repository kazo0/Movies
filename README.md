# Yet Another Movies App
This is a reference project built using [Xamarin.Forms](https://dotnet.microsoft.com/apps/xamarin/xamarin-forms). It is a simple app that uses an API from [The Movie Database](https://developers.themoviedb.org/3/getting-started/introduction) to grab top/upcoming/popular/now playing movies along with their details to display to the user.
This app leverages the [Xamarin.Forms Shell](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/shell/) for tabbed navigation. I chose to use Microsoft's own Dependency Injection (DI) technologies along with the [Generic Host Builder](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1). My approach was partly taken from James Montemagno's [guide](https://montemagno.com/add-asp-net-cores-dependency-injection-into-xamarin-apps-with-hostbuilder/) on using ASP.NET Core's DI library for Xamarin.Forms.
Some of the UI on the Movie Details page was also ~~copied from~~ inspired by Kym Phillpotts' excellent [blog](https://kymphillpotts.com/xamarin-forms-ui-challenge-rottenui.html) on building a Rotten Tomatoes mobile UI

## Architecture
The code is separated into 4 layers, each having only a reference to the layer above it along with a `.Core` library for cross-cutting concerns:

- `Movies.Client`
- `Movies.Services`
- `Movies.Presentation`
- `Movies` (View layer + app entry point)
- `Movies.Core` (Cross-Cutting)

### Client
The `.Client` layer is used as a top-level layer between the application domain and any third-party API, in this case, The Movie Database's API. It exposes an interface specific to The Movie Database (TMDb) called `ITmdbClient` as the models it deals with are specific to the TMDb API. Adding a different third-party provider would require a new client interface and implementation.

### Services
The `.Services` layer is used as the business logic layer. It acts as the intermediary between the `ViewModel`s in the `.Presentation` layer and the `Client`s. Here is where we expose a more general interface of `IMoviesService` that can be consumed without caring who the third-party provider of data is. This is done as a way to de-couple the `.Presentation` layer from ever knowing where the data is coming from. This layer contains all of our business models that all lower layers will use. For the time being, we only have one implementation of `IMoviesService`, the `TmdbMoviesService`, which knows to use its corresponding `TmdbClient`.

### Presentation
The `.Presentation` layer contains all `ViewModel`s that are needed for the lower `View` layer to bind to. This follows the traditional [MVVM Presentation Pattern](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel). For the time being, I chose not to use any third-party MVVM solution like [Prism](https://prismlibrary.com/) or [MvvmCross](https://www.mvvmcross.com/) since I wanted to take a shot at building out my viewmodel solution myself. I do, however, rely on some small, awesome third-party libraries:
- [Fody.PropertyChanged](https://github.com/Fody/PropertyChanged) for making `INotifyPropertChanged` less ugly
- [MvvmHelpers](https://github.com/jamesmontemagno/mvvm-helpers) for `ObservableRangeCollection`s and `AsyncCommand`

Rolling my own MVVM solution did lead to me having a reference to Xamarin.Forms directly in the `.Presentation` layer for navigation purposes which I am not a fan of. This ultimately means that the `.Presentation` layer is coupled to whatever technology is used for the `View`s, which is a no-no. This is something I would like to address in the future since right now I am relying on the Xamarin.Forms Shell to perform route-based navigation instead of `ViewModel`-based navigation.

I tried to keep the code as [DRY](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself) as possible by leveraging abstract classes. Most of the `View`s and `ViewModel`s do pretty much the same thing, get a list of movies to display and allow for paging and refreshing. This is why most of the logic for many of the `ViewModel`s lies within the `PagingViewModel<T>` class that is built to allow for any model object to be displayed as a list and paged. A specific `MoviesPagingViewModel` where `T` is bound to type `Movie` was created in order for me to easily reference the base `ViewModel` in XAML without having to deal with generics in XAML. 

### Views + app entry
The `Movies` layer is used as our `View`s layer along with the startup logic and main app entry point. I didn't want to break these apart since the code-behind of my `View`s rely on having access to the DI Container for resolving their `ViewModel`s. There is currently a bug/missing feature in Xamarin.Forms Shell that does not allow for constructor injection into your `ContentPage`s if they are defined within a `ShellContent`.
