using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConvertToCoordinatesRequest : Request<Address>
    {
        private What3WordsV3 api;
        private string words;

        public ConvertToCoordinatesRequest(What3WordsV3 api, string words)
        {
            this.api = api;
            this.words = words;
        }

        internal override Task<Address> ApiRequest
        {
            get
            {
                return api.request.ConvertToCoordinates(words);
            }
        }
    }
}
