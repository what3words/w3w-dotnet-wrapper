using Refit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.request;

namespace what3words.dotnet.wrapper
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class What3WordsV3
    {
        private static string DEFAULT_ENDPOINT = "https://api.what3words.com/v3";
        private static string HEADER_WHAT3WORDS_API_KEY = "X-Api-Key";
        private static string W3W_WRAPPER = "X-W3W-Wrapper";


        ///<summary>Get a new API manager instance.</summary>
        ///
        ///<param name="apiKey">Your what3words API key obtained from https://what3words.com/select-plan</param>
        public What3WordsV3(string apiKey)
        {
            setupHttpClient(apiKey, DEFAULT_ENDPOINT, null);
        }

        ///<summary>Get a new API manager instance.</summary>
        ///
        ///<param name="apiKey">Your what3words API key obtained from https://what3words.com/select-plan </param>
        ///<param name="endpoint">override the default public API endpoint. </param>
        public What3WordsV3(string apiKey, string endpoint)
        {
            setupHttpClient(apiKey, endpoint, null);
        }

        ///<summary>Get a new API manager instance.</summary>
        ///
        ///<param name="apiKey">Your what3words API key obtained from https://what3words.com/select-plan </param>
        ///<param name="endpoint">override the default public API endpoint. </param>
        ///<param name="headers">add any custom HTTP headers to send in each request</param>
        protected What3WordsV3(string apiKey, string endpoint, Dictionary<string, string> headers)
        {
            setupHttpClient(apiKey, endpoint, headers);
        }

        internal IW3WRequests request { get; set; }

        private void setupHttpClient(string apiKey, string endpoint, Dictionary<string, string> headers)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpoint ?? DEFAULT_ENDPOINT);
            httpClient.DefaultRequestHeaders.Add(W3W_WRAPPER, getUserAgent());
            httpClient.DefaultRequestHeaders.Add(HEADER_WHAT3WORDS_API_KEY, apiKey);

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            request = RestService.For<IW3WRequests>(httpClient);
        }

        private string getUserAgent()
        {
            return "what3words-dotNet/" + (GetType().Assembly.GetName().Version.ToString()) + " ("
                + (Environment.OSVersion) + ")";
        }

        /**
         * <summary>Convert a latitude and longitude to a 3 word address, in the language of your choice. It also returns country,
         * the bounds of the grid square, a nearest place (such as a local town) and a link to our map site.</summary>
         *
         * <param name="coordinates">the latitude and longitude of the location to convert to 3 word address.</param>
         *
         * <returns>a <see cref="ConvertTo3WARequest"/> instance suitable for invoking a `convert-to-3wa` API request</returns>
        */
        public ConvertTo3WARequest ConvertTo3WA(Coordinates coordinates)
        {
            return new ConvertTo3WARequest(this, coordinates);
        }

        /**
         * <summary>Convert a 3 word address to a latitude and longitude. It also returns country, the bounds of the grid square,
         * a nearest place (such as a local town) and a link to our map site.</summary>
         *
         * <param name="words">A 3 word address as a string. It must be three words separated with dots or a Japanese middle dot character (・).
         * Words separated by spaces will be rejected. Optionally, the 3 word address can be prefixed with ///</param>
         *
         * <returns>a <see cref="ConvertToCoordinatesRequest"/> instance suitable for invoking a `available-languages` API request</returns>
        */
        public ConvertToCoordinatesRequest ConvertToCoordinates(string words)
        {
            return new ConvertToCoordinatesRequest(this, words);
        }

        /**
         * <summary>Retrieves a list all available 3 word address languages, including the ISO 639-1 2 letter code, english name and native name.</summary>
         *
         * <returns>a <see cref="AvailableLanguagesRequest"/> instance suitable for invoking a `convert-to-coordinates` API request</returns>
        */
        public AvailableLanguagesRequest AvailableLanguages()
        {
            return new AvailableLanguagesRequest(this);
        }

        /**
         * <summary>Returns a section of the 3m x 3m what3words grid for a bounding box. The bounding box is specified by lat,lng,lat,lng
         * as south,west,north,east.</summary>
         *
         * <param name="southWest"> South West point of the bounding box for which the grid should be returned. The requested box must not exceed 4km
         * from corner to corner. Latitudes must be &gt;= -90 and &lt;= 90, but longitudes are allowed to wrap around 180. To specify a
         * bounding-box that crosses the anti-meridian, use longitude greater than 180.</param>
         * <param name="northEast"> North East point of the bounding box for which the grid should be returned. The requested box must not exceed 4km
         * from corner to corner. Latitudes must be &gt;= -90 and &lt;= 90, but longitudes are allowed to wrap around 180. To specify a
         * bounding-box that crosses the anti-meridian, use longitude greater than 180.</param>
         *
         * <returns>a <see cref="GridSectionRequest"/> instance suitable for invoking a `grid-section` API request</returns>
        */
        public GridSectionRequest GridSection(Coordinates southWest, Coordinates northEast)
        {
            return new GridSectionRequest(this, southWest, northEast);
        }

        /**
         * <summary>AutoSuggest can take a slightly incorrect 3 word address, and suggest a list of valid 3 word addresses. It has powerful
         * features which can, for example, optionally limit results to a country or area, and prefer results which are near the user.
         * For full details, please see the complete API documentation at https://docs.what3words.com/api/v3/#autosuggest </summary>
         *
         * <param name="input"> The full or partial 3 word address to obtain suggestions for. At minimum this must be the first two complete
         * words plus at least one character from the third word.</param>
         * <param name="options"> The autosuggest options and clippings, check available options here https://docs.what3words.com/api/v3/#autosuggest </param>
         *
         * <returns>a <see cref="AutosuggestRequest"/> instance suitable for invoking a `autosuggest` API request</returns>
        */
        public AutosuggestRequest Autosuggest(string input, AutosuggestOptions options = null)
        {
            return new AutosuggestRequest(this, input, options);
        }

        /**
         * <summary>AutoSuggest can take a slightly incorrect 3 word address, and suggest a list of valid 3 word addresses including coordinates. It has powerful
         * features which can, for example, optionally limit results to a country or area, and prefer results which are near the user.
         * For full details, please see the complete API documentation at https://docs.what3words.com/api/v3/#autosuggest </summary>
         *
         * <param name="input"> The full or partial 3 word address to obtain suggestions for. At minimum this must be the first two complete
         * words plus at least one character from the third word.</param>
         * <param name="options"> The autosuggest options and clippings, check available options here https://docs.what3words.com/api/v3/#autosuggest </param>
         *
         * <returns>a <see cref="AutosuggestWithCoordinatesRequest"/> instance suitable for invoking a `autosuggest-with-coordinates` API request</returns>
        */
        public AutosuggestWithCoordinatesRequest AutosuggestWithCoordinates(string input, AutosuggestOptions options = null)
        {
            return new AutosuggestWithCoordinatesRequest(this, input, options);
        }

        /**
         * <summary>AutosuggestSelection enables simple reporting for what3words address selections in accounts.what3words.com.
         * It should be called once in conjunction with autosuggest or autosuggest-with-coordinates.
         * when an end user picks a what3words address from the suggestions presented to them.</summary>
         *
         * <param name="rawInput">The full or partial 3 word address used on the autosuggest or autosuggest-with-coordinates call.</param>
         * <param name="sourceApi">The source of the selected autosuggest, can be 'text' or 'voice'.</param>
         * <param name="words">The 3 word address of the selected suggestion.</param>
         * <param name="rank">The rank of the selected suggestion.</param>
         * <param name="options">The autosuggest options used on the autosuggest or autosuggest-with-coordinates call.</param>
         *
         * <returns>a <see cref="AutosuggestSelectionRequest"/> instance suitable for invoking a `autosuggest-selection` API request</returns>
        */
        public AutosuggestSelectionRequest AutosuggestSelection(string rawInput, string sourceApi, string words, int rank, AutosuggestOptions options = null)
        {
            return new AutosuggestSelectionRequest(this, rawInput, sourceApi, words, rank, options);
        }

        public bool IsPossible3wa(string input)
        {
            string pattern = @"^/*(?:[^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]{1,}[.｡。･・︒។։။۔።।][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]{1,}[.｡。･・︒។։။۔።।][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]{1,}|'<,.>?/"";:£§º©®\s]+[.｡。･・︒។։။۔።।][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]+|[^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]+([\u0020\u00A0][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]+){1,3}[.｡。･・︒។։။۔።।][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]+([\u0020\u00A0][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]+){1,3}[.｡。･・︒។։။۔።।][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]+([\u0020\u00A0][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]+){1,3})$";
            return Regex.IsMatch(input, pattern);
        }

        public IEnumerable<string> FindPossible3wa(string input)
        {
            string pattern = @"[^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]{1,}[.｡。･・︒។։။۔።।][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]{1,}[.｡。･・︒។։။۔።।][^0-9`~!@#$%^&*()+\-_=[{\]}\\|'<,.>?/"";:£§º©®\s]{1,}";
            return Regex.Matches(input, pattern).OfType<Match>().Select(m => m.Value).AsEnumerable();
        }

        public bool IsValid3wa(string input)
        {
            if (IsPossible3wa(input))
            {
                var options = new AutosuggestOptions().SetNResults(1);
                var result = new AutosuggestRequest(this, input, options).RequestAsync().Result;
                if (result.IsSuccessful && result.Data.Suggestions.Count > 0)
                {
                    return result.Data.Suggestions[0].Words.Equals(input, StringComparison.OrdinalIgnoreCase);
                }
            }
            return false;
        }
    }
}
