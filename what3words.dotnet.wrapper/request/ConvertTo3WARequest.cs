using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using what3words.dotnet.wrapper;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
    public class ConvertTo3WARequest {
        private What3WordsV3 api;
        private string coordinates;
        private string language;

        public ConvertTo3WARequest(What3WordsV3 api)
        {
            this.api = api;
        }

        public ConvertTo3WARequest Coordinates(double lat, double lng)
        {
            this.coordinates = lat + "," + lng;
            return this;
        }

        public ConvertTo3WARequest Language(string language)
        {
            this.language = language;
            return this;
        }

        public async Task<ConvertTo3WA> RequestAsync()
        {
            try
            {
                return await api.request.ConvertTo3WA(coordinates);
            }
            catch (ApiException e)
            {
                return await e.GetContentAsAsync<ConvertTo3WA>();
            }
        }
    }
}



public interface IW3WRequests
{
    [Get("/convert-to-3wa?coordinates={coordinates}")]
    Task<ConvertTo3WA> ConvertTo3WA(string coordinates);
}