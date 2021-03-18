using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AutosuggestSelectionRequest
    {
        private What3WordsV3 api;
        private AutosuggestOptions options;
        private string rawInput;
        private string selection;
        private int rank;
        private string sourceApi;

        public AutosuggestSelectionRequest(What3WordsV3 api, string rawInput, string sourceApi, string words, int rank, AutosuggestOptions options)
        {
            this.api = api;
            this.options = options;
            this.rawInput = rawInput;
            this.selection = words;
            this.rank = rank;
            this.sourceApi = sourceApi;
        }

        public async Task<APIResponse> RequestAsync()
        {
            try
            {
                await api.request.AutoSuggestSelection(rawInput, selection, rank, sourceApi, options);
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
                    var error = new APIError();
                    error.Code = What3WordsError.UnknownError.ToString();
                    error.Message = e.Message;
                    return new APIResponse(error);
                }
            }
            catch (Exception e)
            {
                var error = new APIError();
                error.Code = What3WordsError.NetworkError.ToString();
                error.Message = e.Message;
                return new APIResponse(error);
            }
        }
    }
}