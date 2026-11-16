using System;
using System.Net.Http;
using System.Threading.Tasks;
using MovieCollection.TVMaze;
using MovieCollection.TVMaze.Models;

namespace Demo
{
    internal class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        // See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient

        private static readonly HttpClient _httpClient = new();

        // Note: The 'ApiKey' is optional.
        private static readonly TVMazeService _service = new(_httpClient);

        private static async Task Main()
        {
Start:
            Console.Clear();
            Console.WriteLine("Welcome to the TVMaze demo.\n");

            string[] items =
            [
                "1. Show search",
                "2. Show single search",
                "3. Show search: IMDb Id",
                "4. Show search: TVDb Id",
                "5. Show search: TVRage Id",
                "6. People search",
                "7. Schedule",
                "8. Web/streaming schedule",
                "9. Show main information",
                "10. Show episode list",
                "11. Episode by number",
                "12. Episodes by date",
                "13. Show seasons",
                "14. Season episodes",
                "15. Show cast",
                "16. Show crew",
                "17. Show AKA's (aliases)",
                "18. Show Images",
                "19. Episode main information",
                "20. Person main information",
                "21. Person cast credits",
                "22. Person crew credits",
            ];

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.Write("\nPlease select an option: ");
            int input = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            var task = input switch
            {
                2 => SearchSingleShow(),
                3 => SearchByIMDbId(),
                4 => SearchByTVDbId(),
                5 => SearchByTVRageId(),
                6 => SearchPeople(),
                7 => GetSchedule(),
                8 => GetWebSchedule(),
                9 => GetShowInfo(),
                10 => GetShowEpisodes(),
                11 => GetShowEpisode(),
                12 => GetShowEpisodesByDate(),
                13 => GetShowSeasons(),
                14 => GetSeasonEpisodes(),
                15 => GetShowCast(),
                16 => GetShowCrew(),
                17 => GetShowAliases(),
                18 => GetShowImages(),
                19 => GetEpisodeInfo(),
                20 => GetPersonInfo(),
                21 => GetCastCredits(),
                22 => GetCrewCredits(),
                _ => SearchShows(),
            };

            await task;

            // Wait for user to Exit
            Console.WriteLine("Press any key to go back to menu...");
            Console.ReadKey();

            goto Start;
        }

        private static async Task SearchShows()
        {
            var result = await _service.SearchShowsAsync("fleabag");

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Console.WriteLine("Score: {0}", item.Score);
                Print(item.Show);
            }
        }

        private static async Task SearchSingleShow()
        {
            var result = await _service.SearchSingleShowAsync("black books");

            if (result.IsSuccess && result.Response is not null)
            {
                Print(result.Response);
            }
        }

        private static async Task SearchByIMDbId()
        {
            // BoJack Horseman
            var result = await _service.SearchByIMDbIdAsync("tt3398228");

            if (result.IsSuccess && result.Response is not null)
            {
                Print(result.Response);
            }
        }

        private static async Task SearchByTVDbId()
        {
            // Solar Opposites
            var result = await _service.SearchByTVDbIdAsync("375892");

            if (result.IsSuccess && result.Response is not null)
            {
                Print(result.Response);
            }
        }

        private static async Task SearchByTVRageId()
        {
            var result = await _service.SearchByTVRageIdAsync("24493");

            if (result.IsSuccess && result.Response is not null)
            {
                Print(result.Response);
            }
        }

        private static async Task SearchPeople()
        {
            var result = await _service.SearchPeopleAsync("cate");

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Console.WriteLine("Score: {0}", item.Score);
                Print(item.Person);
            }
        }

        private static async Task GetSchedule()
        {
            // Common mistake: if you want UK's schedule pass "GB" as country.
            var result = await _service.GetScheduleAsync(country: "US");

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetWebSchedule()
        {
            var result = await _service.GetStreamingScheduleAsync(country: "US");

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetShowInfo()
        {
            var result = await _service.GetShowInfoAsync(2);

            if (result.IsSuccess && result.Response is not null)
            {
                Print(result.Response);
            }
        }

        private static async Task GetShowEpisodes()
        {
            var result = await _service.GetShowEpisodesListAsync(16149);

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetShowEpisode()
        {
            var result = await _service.GetShowEpisodeAsync(16149, 2, 3);

            if (result.IsSuccess && result.Response is not null)
            {
                Print(result.Response);
            }
        }

        private static async Task GetShowEpisodesByDate()
        {
            var result = await _service.GetShowEpisodesByDateAsync(17078, new DateTime(2019, 04, 12));

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetShowSeasons()
        {
            var result = await _service.GetShowSeasonsAsync(17078);

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetSeasonEpisodes()
        {
            var result = await _service.GetSeasonEpisodesAsync(82424);

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetShowCast()
        {
            var result = await _service.GetShowCastAsync(530);

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetShowCrew()
        {
            var result = await _service.GetShowCrewAsync(530);

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetShowAliases()
        {
            var result = await _service.GetShowAliasesAsync(171);

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetShowImages()
        {
            var result = await _service.GetShowImagesAsync(269);

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetEpisodeInfo()
        {
            var result = await _service.GetEpisodeByIdAsync(48221);

            if (result.IsSuccess && result.Response is not null)
            {
                Print(result.Response);
            }
        }

        private static async Task GetPersonInfo()
        {
            var result = await _service.GetPersonInfoAsync(37759);

            if (result.IsSuccess && result.Response is not null)
            {
                Print(result.Response);
            }
        }

        private static async Task GetCastCredits()
        {
            var result = await _service.GetCastCreditsAsync(40598, "show", "character");

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static async Task GetCrewCredits()
        {
            var result = await _service.GetCrewCreditsAsync(100, "show");

            if (!result.IsSuccess || result.Response is null)
            {
                Console.WriteLine("IsSuccess: {0}", result.IsSuccess);
                Console.WriteLine("StatusCode: {0}", result.StatusCode);

                return;
            }

            foreach (var item in result.Response)
            {
                Print(item);
            }
        }

        private static void Print(Show item)
        {
            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", item.Network?.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
            Console.WriteLine();
        }

        private static void Print(Person item)
        {
            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Image: {0}", item.Image?.Original);
            Console.WriteLine("Birthday: {0}", item.Birthday);
            Console.WriteLine("Updated: {0}", item.Updated);
            Console.WriteLine();
        }

        private static void Print(Schedule item)
        {
            var showName = item.Show?.Name ?? item.Embedded?.Show?.Name;
            var networkName = item.Show?.Network?.Name ?? item.Embedded?.Show?.WebChannel?.Name;

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Show Name: {0}", showName);
            Console.WriteLine("Episode Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", networkName);
            Console.WriteLine("Summary: {0}", item.Summary);
            Console.WriteLine();
        }

        private static void Print(Episode item)
        {
            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Season: {0}", item.Season);
            Console.WriteLine("Number: {0}", item.Number);
            Console.WriteLine("Summary: {0}", item.Summary);
            Console.WriteLine();
        }

        private static void Print(Season item)
        {
            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Number: {0}", item.Number);
            Console.WriteLine("PremiereDate: {0}", item.PremiereDate);
            Console.WriteLine("Summary: {0}", item.Summary);
            Console.WriteLine();
        }

        private static void Print(Cast item)
        {
            Console.WriteLine("Person Id: {0}", item.Person?.Id);
            Console.WriteLine("Person Name: {0}", item.Person?.Name);
            Console.WriteLine("Person Image: {0}", item.Person?.Image?.Original);
            Console.WriteLine("Character Name: {0}", item.Character?.Name);
            Console.WriteLine();
        }

        private static void Print(Crew item)
        {
            Console.WriteLine("Type: {0}", item.Type);
            Console.WriteLine("Person Id: {0}", item.Person?.Id);
            Console.WriteLine("Person Name: {0}", item.Person?.Name);
            Console.WriteLine("Person Image: {0}", item.Person?.Image?.Original);
            Console.WriteLine();
        }

        private static void Print(ShowAlias item)
        {
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Country: {0}", item.Country?.Name);
            Console.WriteLine();
        }

        private static void Print(ShowImages item)
        {
            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Type: {0}", item.Type);
            Console.WriteLine("Main: {0}", item.Main);

            if (item.Resolutions?.Original != null)
            {
                Console.WriteLine("\n- Original:\n");
                Console.WriteLine("Url: {0}", item.Resolutions.Original.Url);
                Console.WriteLine("Width: {0}", item.Resolutions.Original.Width);
                Console.WriteLine("Height: {0}", item.Resolutions.Original.Height);
            }

            if (item.Resolutions?.Medium != null)
            {
                Console.WriteLine("\n- Medium:\n");
                Console.WriteLine("Url: {0}", item.Resolutions.Medium.Url);
                Console.WriteLine("Width: {0}", item.Resolutions.Medium.Width);
                Console.WriteLine("Height: {0}", item.Resolutions.Medium.Height);
            }

            Console.WriteLine();
        }

        private static void Print(CastCredits item)
        {
            Console.WriteLine("Show Name: {0}", item.Embedded?.Show?.Name);
            Console.WriteLine("Character Name: {0}", item.Embedded?.Character?.Name);
            Console.WriteLine();
        }

        private static void Print(CrewCredits item)
        {
            Console.WriteLine("Type: {0}", item.Type);
            Console.WriteLine("Show Name: {0}", item.Embedded?.Show?.Name);
            Console.WriteLine();
        }
    }
}
