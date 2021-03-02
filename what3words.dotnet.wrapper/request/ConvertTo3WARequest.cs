using Refit;
using System;
using System.Net;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
    public class ConvertTo3WARequest {
        public class ConvertTo3WAParams
        {
            [AliasAs("coordinates")]
            public string Coordinates { get; set; }
            [AliasAs("language")]
            public string Language { get; set; }
        }

        private What3WordsV3 api;
        private ConvertTo3WAParams queryParams;

        public ConvertTo3WARequest(What3WordsV3 api)
        {
            this.api = api;
            this.queryParams = new ConvertTo3WAParams();
        }

        public ConvertTo3WARequest Coordinates(Coordinates coordinates)
        {
            queryParams.Coordinates = coordinates.Lat + "," + coordinates.Lng;
            return this;
        }

        public ConvertTo3WARequest Language(string language)
        {
            queryParams.Language = language;
            return this;
        }

        public async Task<APIResponse<Address>> RequestAsync()
        {
            try
            {
                return new APIResponse<Address>(await api.request.ConvertTo3WA(queryParams));
            }
            catch (Refit.ApiException e)
            {
                var apiException = await e.GetContentAsAsync<response.ApiException>();
                return new APIResponse<Address>(apiException.Error);
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
