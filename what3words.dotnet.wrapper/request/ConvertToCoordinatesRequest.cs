using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConvertToCoordinatesRequest : Request<Address>
    {
        private readonly What3WordsV3 _api;
        private readonly string _words;

        public ConvertToCoordinatesRequest(What3WordsV3 api, string words)
        {
            _api = api;
            _words = words;
        }

        protected override Task<Address> ApiRequest
        {
            get
            {
                return _api.Request.ConvertToCoordinates(_words);
            }
        }
    }
}
