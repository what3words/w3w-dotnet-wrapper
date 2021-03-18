using System.Threading.Tasks;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AutosuggestWithCoordinatesRequest : Request<AutoSuggestWithCoordinates>
    {
        internal What3WordsV3 api;
        internal AutosuggestOptions options;
        internal string input;

        public AutosuggestWithCoordinatesRequest(What3WordsV3 api, string input, AutosuggestOptions options)
        {
            this.api = api;
            this.options = options;
            this.input = input;
        }

        internal override Task<AutoSuggestWithCoordinates> ApiRequest
        {
            get
            {
                return api.request.AutoSuggestWithCoordinates(input, options);
            }
        }
    }
}