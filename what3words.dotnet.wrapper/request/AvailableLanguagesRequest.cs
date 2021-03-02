using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
    public class AvailableLanguagesRequest
    {
        private What3WordsV3 api;

        public AvailableLanguagesRequest(What3WordsV3 api)
        {
            this.api = api;
        }

        public async Task<APIResponse<AvailableLanguages>> RequestAsync()
        {
            try
            {
                return new APIResponse<AvailableLanguages>(await api.request.AvailableLanguages());
            }
            catch (Refit.ApiException e)
            {
                var apiException = await e.GetContentAsAsync<ApiException>();
                return new APIResponse<AvailableLanguages>(apiException.Error);
            }
            catch (Exception e)
            {
                var error = new APIError();
                error.Code = What3WordsError.NetworkError.ToString();
                error.Message = e.Message;
                return new APIResponse<AvailableLanguages>(error);
            }
        }
    }
}
