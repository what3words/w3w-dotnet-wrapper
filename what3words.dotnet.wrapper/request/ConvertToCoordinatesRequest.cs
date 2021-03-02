using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
    public class ConvertToCoordinatesRequest
    {
        private What3WordsV3 api;
        private string words;

        public ConvertToCoordinatesRequest(What3WordsV3 api, string words)
        {
            this.api = api;
            this.words = words;
        }

        public async Task<APIResponse<Address>> RequestAsync()
        {
            try
            {
                return new APIResponse<Address>(await api.request.ConvertToCoordinates(words));
            }
            catch (Refit.ApiException e)
            {
                var exception = await e.GetContentAsAsync<ApiException>();
                return new APIResponse<Address>(exception.Error);
            }
            catch (Exception e)
            {
                var error = new APIError();
                error.Code = What3WordsError.NetworkError.ToString();
                error.Message = e.Message;
                return new APIResponse<Address>(error);
            }
        }
    }
}
