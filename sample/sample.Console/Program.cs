/**
 * Simple console application using the what3words-dotnet-wrapper.
 */

using sample.Console.Utils;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using what3words.dotnet.wrapper;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.request;

namespace sample.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var arguments = new Arguments(args);
                if (arguments.GetCommand().Equals("help", System.StringComparison.Ordinal))
                {
                    PrintUsage();
                    return;
                }
                var apiKey = arguments.GetArgument("--api-key");
                if (apiKey != null)
                {
                    var api = new What3WordsV3(apiKey);
                    switch (arguments.GetCommand())
                    {

                        case "convert-to-coordinates":
                            ConvertToCoordinates(api, arguments);
                            break;
                        case "convert-to-3wa":
                            ConvertTo3WA(api, arguments);
                            break;
                        case "autosuggest":
                            AutoSuggest(api, arguments);
                            break;
                        case "autosuggest-with-coordinates":
                            AutosuggestWithCoordinates(api, arguments);
                            break;
                        case "autosuggest-selection":
                            AutoSuggestSelection(api, arguments);
                            break;
                        case "available-languages":
                            AvailableLanguages(api);
                            break;
                        case "grid-section":
                            GridSection(api, arguments);
                            break;
                        case "is-possible-3wa":
                            IsPossible3wa(api, arguments);
                            break;
                        case "find-possible-3wa":
                            FindPossible3wa(api, arguments);
                            break;
                        case "is-valid-3wa":
                            IsValid3wa(api, arguments);
                            break;
                        default:
                            System.Console.WriteLine("Command is not supported.");
                            PrintUsage();
                            break;
                    }
                }
            }
            catch (System.Exception error)
            {
                System.Console.WriteLine("Something went wrong, " + error.Message);
                PrintUsage();
            }
        }

        private static void PrintUsage()
        {
            System.Console.WriteLine("Usage: <command> [options]");
            System.Console.WriteLine("Required parameters:");
            System.Console.WriteLine("  --api-key <key>");
            System.Console.WriteLine("Commands:");
            System.Console.WriteLine("  convert-to-coordinates --3wa <3 word address>");
            System.Console.WriteLine("  convert-to-3wa --lat <latitude> --lng <longitude>");
            System.Console.WriteLine("  grid-section --sw-lat <latitude> --sw-lng <longitude> --ne-lat <latitude> --ne-lng <longitude>");
            System.Console.WriteLine("  autosuggest --input <input>");
            System.Console.WriteLine("  autosuggest-with-coordinates --input <input>");
            System.Console.WriteLine("  autosuggest-selection --raw-input <input> --source-api <source-api> --words <words> --rank <rank>");
            System.Console.WriteLine("  available-languages");
            System.Console.WriteLine("  is-possible-3wa --words <words>");
            System.Console.WriteLine("  find-possible-3wa --words <words>");
            System.Console.WriteLine("  is-valid-3wa --words <words>");
        }

        private static void ConvertToCoordinates(What3WordsV3 api, Arguments arg)
        {
            var threeWords = arg.GetArgument("--3wa");
            var result = api.ConvertToCoordinates(threeWords).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                System.Console.WriteLine("Coordinates: " + result.Data.Coordinates.Lat + ", " + result.Data.Coordinates.Lng);
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void ConvertTo3WA(What3WordsV3 api, Arguments arg)
        {
            var lat = double.TryParse(arg.GetArgument("--lat"), NumberStyles.Any, CultureInfo.InvariantCulture, out var latitude) ? latitude : 0.0;
            var lng = double.TryParse(arg.GetArgument("--lng"), NumberStyles.Any, CultureInfo.InvariantCulture, out var longitude) ? longitude : 0.0;
            var coordinates = new Coordinates(lat, lng);
            var result = api.ConvertTo3WA(coordinates).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                System.Console.WriteLine($"3 word address: https://w3w.co/{result.Data.Words}");
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void AutoSuggest(What3WordsV3 api, Arguments arg)
        {
            var input = arg.GetArgument("--input");
            var result = api.Autosuggest(input).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                if (result.Data.Suggestions.Count > 0)
                {
                    System.Console.WriteLine("Suggestions: " + string.Join(", ", result.Data.Suggestions.Select(x => $"https://w3w.co/{x.Words}")));
                }
                else
                {
                    System.Console.WriteLine("No suggestions found.");
                }
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void AutoSuggestSelection(What3WordsV3 api, Arguments arg)
        {
            var input = arg.GetArgument("--raw-input");
            var sourceApi = arg.GetArgument("--source-api");
            var words = arg.GetArgument("--words");
            var rank = int.TryParse(arg.GetArgument("--rank"), out var r) ? r : 0;
            var options = new AutosuggestOptions();
            var result = api.AutosuggestSelection(input, sourceApi, words, rank, options).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                System.Console.WriteLine($"Suggestion for {words} selected");
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void AvailableLanguages(What3WordsV3 api)
        {
            var result = api.AvailableLanguages().RequestAsync().Result;
            if (result.IsSuccessful)
            {
                System.Console.WriteLine("Languages: " + string.Join(", ", result.Data.Languages.Select(x => $"{x.Name} [{x.Code}]")));
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void GridSection(What3WordsV3 api, Arguments arg)
        {
            var southWestLat = arg.GetArgument("--sw-lat");
            var southWestLng = arg.GetArgument("--sw-lng");
            var northEastLat = arg.GetArgument("--ne-lat");
            var northEastLng = arg.GetArgument("--ne-lng");
            var southWest = new Coordinates(double.Parse(southWestLat, CultureInfo.InvariantCulture), double.Parse(southWestLng, CultureInfo.InvariantCulture));
            var northEast = new Coordinates(double.Parse(northEastLat, CultureInfo.InvariantCulture), double.Parse(northEastLng, CultureInfo.InvariantCulture));
            var result = api.GridSection(southWest, northEast).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                System.Console.WriteLine("Grid section: " + string.Join(", ", result.Data.Lines.Select(x => x.Start.Lat + ", " + x.Start.Lng + " - " + x.End.Lat + ", " + x.End.Lng)));
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void AutosuggestWithCoordinates(What3WordsV3 api, Arguments arg)
        {
            var input = arg.GetArgument("--input");
            var result = api.AutosuggestWithCoordinates(input).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                System.Console.WriteLine("Suggestions: " + string.Join(", ", result.Data.Suggestions.Select(x => x.Words + " (" + x.Coordinates.Lat + ", " + x.Coordinates.Lng + ")")));
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void IsPossible3wa(What3WordsV3 api, Arguments arg)
        {
            var words = arg.GetArgument("--words");
            var result = api.IsPossible3wa(words);
            if (result)
            {
                System.Console.WriteLine($"\"{words}\" is possibly a 3 word address");
            }
            else
            {
                System.Console.WriteLine($"\"{words}\" is not a possible 3 word address");
            }
        }

        private static void FindPossible3wa(What3WordsV3 api, Arguments arg)
        {
            var words = arg.GetArgument("--words");
            var result = api.FindPossible3wa(words);
            if (result.Any())
            {
                System.Console.WriteLine("Possible 3 word addresses: " + string.Join(", ", result));
            }
            else
            {
                System.Console.WriteLine("No possible 3 word addresses found.");
            }
        }

        private static void IsValid3wa(What3WordsV3 api, Arguments arg)
        {
            var words = arg.GetArgument("--words");
            var result = api.IsValid3wa(words);
            if (result)
            {
                System.Console.WriteLine($"\"{words}\" is a valid 3 word address");
            }
            else
            {
                System.Console.WriteLine($"\"{words}\" is not a valid 3 word address");
            }
        }
    }
}