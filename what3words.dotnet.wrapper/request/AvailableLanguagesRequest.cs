using System.Threading.Tasks;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AvailableLanguagesRequest : Request<AvailableLanguages>
    {
        private readonly What3WordsV3 _api;

        public AvailableLanguagesRequest(What3WordsV3 api)
        {
            _api = api;
        }

        protected override Task<AvailableLanguages> ApiRequest
        {
            get
            {
                return _api.Request.AvailableLanguages();
            }
        }
    }
}
