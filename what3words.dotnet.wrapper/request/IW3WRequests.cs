﻿using Refit;
using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.request;
using what3words.dotnet.wrapper.response;
using static what3words.dotnet.wrapper.request.ConvertTo3WARequest;


namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IW3WRequests
    {

        [Get("/convert-to-3wa")]
        Task<Address> ConvertTo3WA(ConvertTo3WAOptions options);

        [Get("/convert-to-coordinates?words={words}")]
        Task<Address> ConvertToCoordinates(string words);

        [Get("/available-languages")]
        Task<AvailableLanguages> AvailableLanguages();

        [Get("/grid-section?bounding-box={boundingBox}")]
        Task<GridSection> GridSection(string boundingBox);

        [Get("/autosuggest")]
        Task<AutoSuggest> AutoSuggest(string input, AutosuggestOptions options);

        [Get("/autosuggest-with-coordinates")]
        Task<AutoSuggestWithCoordinates> AutoSuggestWithCoordinates(string input, AutosuggestOptions options);

        [Get("/autosuggest-selection?raw-input={rawInput}&selection={selection}&rank={rank}&source-api={sourceApi}")]
        Task AutoSuggestSelection(string rawInput, string selection, int? rank, string sourceApi, AutosuggestOptions options);
    }

    public abstract class Request<T>
    {
        protected abstract Task<T> ApiRequest { get; }

        /**
           * <summary> Request API call asynchronously </summary>
           */
        public async Task<APIResponse<T>> RequestAsync()
        {
            try
            {
                return new APIResponse<T>(await ApiRequest);
            }
            catch (Refit.ApiException e)
            {
                var apiException = await e.GetContentAsAsync<what3words.dotnet.wrapper.response.ApiException>();
                if (apiException != null)
                {
                    return new APIResponse<T>(apiException.Error);
                }
                else
                {
                    var error = new APIError
                    {
                        Code = What3WordsError.UnknownError.ToString(),
                        Message = e.Message
                    };
                    return new APIResponse<T>(error);
                }
            }
            catch (Exception e)
            {
                var error = new APIError
                {
                    Code = What3WordsError.NetworkError.ToString(),
                    Message = e.Message
                };
                return new APIResponse<T>(error);
            }
        }
    }
}