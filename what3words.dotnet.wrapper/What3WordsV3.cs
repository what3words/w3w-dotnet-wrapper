using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
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

        public What3WordsV3(string apiKey)
        {
            setupHttpClient(apiKey, DEFAULT_ENDPOINT, null);
        }

        public What3WordsV3(string apiKey, string endpoint)
        {
            setupHttpClient(apiKey, endpoint, null);
        }

        protected What3WordsV3(string apiKey, string endpoint, Dictionary<string, string> headers)
        {
            setupHttpClient(apiKey, endpoint, headers);
        }

        internal IW3WRequests request { get; set; }

        private void setupHttpClient(string apiKey, string endpoint, Dictionary<string, string> headers)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpoint);
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

        public ConvertTo3WARequest ConvertTo3WA(Coordinates coordinates)
        {
            return new ConvertTo3WARequest(this, coordinates);
        }

        public ConvertToCoordinatesRequest ConvertToCoordinates(string words)
        {
            return new ConvertToCoordinatesRequest(this, words);
        }

        public AvailableLanguagesRequest AvailableLanguages()
        {
            return new AvailableLanguagesRequest(this);
        }

        public GridSectionRequest GridSection(Square square)
        {
            return new GridSectionRequest(this, square);
        }

        public AutosuggestRequest Autosuggest(string input, AutosuggestOptions options = null)
        {
            return new AutosuggestRequest(this, input, options);
        }

        public AutosuggestWithCoordinatesRequest AutosuggestWithCoordinates(string input, AutosuggestOptions options = null)
        {
            return new AutosuggestWithCoordinatesRequest(this, input, options);
        }

        public AutosuggestSelectionRequest AutosuggestSelection(string rawInput, string sourceApi, Suggestion seletedSuggestion, AutosuggestOptions options = null)
        {
            return new AutosuggestSelectionRequest(this, rawInput, sourceApi, seletedSuggestion, options);
        }
    }
}
