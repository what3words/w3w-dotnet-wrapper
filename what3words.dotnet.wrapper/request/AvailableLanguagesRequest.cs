using System.Threading.Tasks;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AvailableLanguagesRequest : Request<AvailableLanguages>
    {
        private What3WordsV3 api;

        public AvailableLanguagesRequest(What3WordsV3 api)
        {
            this.api = api;
        }

        internal override Task<AvailableLanguages> ApiRequest
        {
            get
            {
                return api.request.AvailableLanguages();
            }
        }
    }
}
