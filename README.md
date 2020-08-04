# Yet Another Movies App
This is a reference project built using [Xamarin.Forms](https://dotnet.microsoft.com/apps/xamarin/xamarin-forms). It is a simple app that uses an API from [The Movie Database](https://developers.themoviedb.org/3/getting-started/introduction) to grab top/upcoming/popular/now playing movies along with their details to display to the user.
This app leverages the [Xamarin.Forms Shell](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/shell/) for tabbed navigation. I chose to use Microsoft's own Dependency Injection (DI) technologies along with the [Generic Host Builder](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1). My approach was partly taken from James Montemagno's [guide](https://montemagno.com/add-asp-net-cores-dependency-injection-into-xamarin-apps-with-hostbuilder/) on using ASP.NET Core's DI library for Xamarin.Forms.
Some of the UI on the Movie Details page was also inspired by Kym Phillpotts' excellent [blog](https://kymphillpotts.com/xamarin-forms-ui-challenge-rottenui.html) on building a Rotten Tomatoes mobile UI

## Architecture
The code is separated into 4 layers, each having only a reference to the layer above it along with a `.Core` library for cross-cutting concerns:

- `Movies.Client`
- `Movies.Services`
- `Movies.Presentation`
- `Movies` (View layer + app entry point)
- `Movies.Core` (Cross-Cutting)

### Client
The `.Client` layer is used as top-level layer between the application domain and any third-party API, in this case, The Movie Database's API. It exposes an interface specific to The Movie Database (TMDb) called `ITmdbClient` as the models it deals with a specific to the TMDb API. Adding a different third-party provider would require new client interface and implementation.

### Services
The `.Services` layer is used as the business logic layer. It acts as the intermediary between the `ViewModel`s in the `.Presentation` layer and the `Client`s. Here is where we expose a more general interface of `IMoviesService` that can be consumed without caring who the third-party provider of data is. This is done as a way to de-couple the `.Presentation` layer from ever knowing where the data is coming from. This layer contains all of our business models that all lower layers will use. For the time being, we only have one implementation of `IMoviesService`, the `TmdbMoviesService`, which knows to use its corresponding Client.
