using System.Threading.Tasks;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AutosuggestRequest : Request<AutoSuggest>
    {
        internal What3WordsV3 _api;
        internal AutosuggestOptions _options;
        internal string _input;

        public AutosuggestRequest(What3WordsV3 api, string input, AutosuggestOptions options)
        {
            _api = api;
            _options = options;
            _input = input;
        }

        protected override Task<AutoSuggest> ApiRequest
        {
            get
            {
                return _api.Request.AutoSuggest(_input, _options);
            }
        }
    }
}