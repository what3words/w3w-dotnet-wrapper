using System.Threading.Tasks;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AutosuggestWithCoordinatesRequest : Request<AutoSuggestWithCoordinates>
    {
        private readonly What3WordsV3 _api;
        private readonly AutosuggestOptions _options;
        private readonly string _input;

        public AutosuggestWithCoordinatesRequest(What3WordsV3 api, string input, AutosuggestOptions options)
        {
            _api = api;
            _options = options;
            _input = input;
        }

        protected override Task<AutoSuggestWithCoordinates> ApiRequest
        {
            get
            {
                return _api.Request.AutoSuggestWithCoordinates(_input, _options);
            }
        }
    }
}