using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using what3words.dotnet.wrapper.exceptions;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;
using static what3words.dotnet.wrapper.request.ConvertTo3WARequest;


namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IW3WRequests
    {

        Task<Address> ConvertTo3WA(ConvertTo3WAOptions options);

        Task<Address> ConvertToCoordinates(string words);

        Task<AvailableLanguages> AvailableLanguages();

        Task<GridSection> GridSection(string boundingBox);

        Task<AutoSuggest> AutoSuggest(string input, AutosuggestOptions options);

        Task<AutoSuggestWithCoordinates> AutoSuggestWithCoordinates(string input, AutosuggestOptions options);

        Task AutoSuggestSelection(string rawInput, string selection, int? rank, string sourceApi, AutosuggestOptions options);
    }

    internal class W3WRequests : IW3WRequests
    {
        private readonly HttpClient httpClient;
        public W3WRequests(HttpClient client)
        {
            httpClient = client;
        }

        public Task<AutoSuggest> AutoSuggest(string input, AutosuggestOptions options)
        {
            var query = options?.ToQuery(true) ?? "";
            return GetAsync<AutoSuggest>($"autosuggest?input={input}{query}");
        }

        public Task AutoSuggestSelection(string rawInput, string selection, int? rank, string sourceApi, AutosuggestOptions options)
        {
            var query = options?.ToQuery(true) ?? "";
            return GetAsync<object>($"autosuggest-selection?raw-input={rawInput}&selection={selection}&rank={rank}&source-api={sourceApi}{query}");
        }

        public Task<AutoSuggestWithCoordinates> AutoSuggestWithCoordinates(string input, AutosuggestOptions options)
        {
            var query = options?.ToQuery(true) ?? "";
            return GetAsync<AutoSuggestWithCoordinates>($"autosuggest-with-coordinates?input={input}{query}");
        }

        public Task<AvailableLanguages> AvailableLanguages()
        {
            return GetAsync<AvailableLanguages>("available-languages");
        }

        public Task<Address> ConvertTo3WA(ConvertTo3WAOptions options)
        {
            var query = options?.ToQuery() ?? "";
            return GetAsync<Address>($"convert-to-3wa{query}");
        }

        public Task<Address> ConvertToCoordinates(string words)
        {
            return GetAsync<Address>($"convert-to-coordinates?words={words}");
        }

        public Task<GridSection> GridSection(string boundingBox)
        {
            return GetAsync<GridSection>($"grid-section?bounding-box={boundingBox}");
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                var apiException = JsonConvert.DeserializeObject<APIResponse>(errorResponse);
                throw new ApiException<APIError>(What3WordsError.NetworkError.ToString(), apiException?.Error);
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }

    public abstract class Request<T>
    {
        protected abstract Task<T> ApiRequest { get; }

        /// <summary>
        /// Request API call asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse<T>> RequestAsync()
        {
            try
            {
                var response = await ApiRequest;
                return new APIResponse<T>(response);
            }
            catch (ApiException<APIError> e)
            {
                return new APIResponse<T>(new APIError
                {
                    Code = e.Error.Code,
                    Message = e.Error.Message
                });
            }
            catch (Exception e)
            {
                var error = new APIError
                {
                    Code = What3WordsError.UnknownError.ToString(),
                    Message = e.Message
                };
                return new APIResponse<T>(error);
            }
        }

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class QueryStringAttribute : Attribute
    {
        public string Alias { get; }
        public QueryStringAttribute(string alias)
        {
            Alias = alias;
        }
    }

    public abstract class URLQueryable
    {
        public string ToQuery(bool appended = false)
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var keyValuePairs = new List<string>();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(this);
                if (value == null) continue;

                var attribute = prop.GetCustomAttribute<QueryStringAttribute>();
                var key = attribute != null ? attribute.Alias : prop.Name;

                var encodedKey = HttpUtility.UrlEncode(key.ToLower());
                var encodedValue = HttpUtility.UrlEncode(value.ToString().ToLower());

                keyValuePairs.Add($"{encodedKey}={encodedValue}");
            }

            return $"{(appended ? "&" : "?")}{string.Join("&", keyValuePairs)}";
        }
    }
}