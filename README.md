# TVMaze API
Unofficial implementation of the TVMaze API for .NET

[![NuGet Version][nuget-shield]][nuget]
[![NuGet Downloads][nuget-shield-dl]][nuget]

## Installation
You can install this package via the `Package Manager Console` in Visual Studio.

```powershell
Install-Package MovieCollection.TVMaze -PreRelease
```

## Configuration
Get or create a new static `HttpClient` instance if you don't have one already.

```csharp
// HttpClient lifecycle management best practices:
// https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
private static readonly HttpClient httpClient = new HttpClient();
```

Then, you need to create the service.

```csharp
// using MovieCollection.TVMaze;

var service = new TVMazeService(httpClient);
```

**Alternatively,** you can create a new `TVMazeOptions` class to set your options.

```csharp
var options = new TVMazeOptions
{
    ApiKey = "your-api-key",
};

var service = new TVMazeService(httpClient, options);
```

## Schedule
You can get the tv schedule via the `GetScheduleAsync` method.

```csharp
var results = await service.GetScheduleAsync(country: "GB");
```

## Streaming Schedule
You can get the streaming schedule via the `GetStreamingScheduleAsync` method.

```csharp
var results = await service.GetStreamingScheduleAsync(country: "US");
```

Please see the demo project for more examples.

## Limitations
- Premium capabilities has not been implemented.
- Show Types, Genres, etc. are not strongly typed.

## Notes
- Please read the [TVMaze][tvmaze]'s [API Licensing][tvmaze-license] and [terms of service][tvmaze-terms] before using their services.

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieCollection.TVMaze
[nuget-shield]: https://img.shields.io/nuget/v/MovieCollection.TVMaze.svg?label=NuGet
[nuget-shield-dl]: https://img.shields.io/nuget/dt/MovieCollection.TVMaze?label=Downloads&color=red

[tvmaze]: https://www.tvmaze.com
[tvmaze-terms]: https://www.tvmaze.com/site/tos
[tvmaze-license]: https://www.tvmaze.com/api#licensing
