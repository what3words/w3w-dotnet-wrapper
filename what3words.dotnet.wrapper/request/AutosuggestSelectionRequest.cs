using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AutosuggestSelectionRequest
    {
        private readonly What3WordsV3 _api;
        private readonly AutosuggestOptions _options;
        private readonly string _rawInput;
        private readonly string _selection;
        private readonly int _rank;
        private readonly string _sourceApi;

        public AutosuggestSelectionRequest(What3WordsV3 api, string rawInput, string sourceApi, string words, int rank, AutosuggestOptions options)
        {
            _api = api;
            _options = options;
            _rawInput = rawInput;
            _selection = words;
            _rank = rank;
            _sourceApi = sourceApi;
        }

        public async Task<APIResponse> RequestAsync()
        {
            try
            {
                await _api.Request.AutoSuggestSelection(_rawInput, _selection, _rank, _sourceApi, _options);
                return new APIResponse();
            }
            catch (Refit.ApiException e)
            {
                var apiException = await e.GetContentAsAsync<ApiException>();
                if (apiException != null)
                {
                    return new APIResponse(apiException.Error);
                }
                else
                {
                    var error = new APIError
                    {
                        Code = What3WordsError.UnknownError.ToString(),
                        Message = e.Message
                    };
                    return new APIResponse(error);
                }
            }
            catch (Exception e)
            {
                var error = new APIError
                {
                    Code = What3WordsError.NetworkError.ToString(),
                    Message = e.Message
                };
                return new APIResponse(error);
            }
        }
    }
}