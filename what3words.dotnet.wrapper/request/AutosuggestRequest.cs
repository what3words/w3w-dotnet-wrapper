﻿using System.Threading.Tasks;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AutosuggestRequest : Request<AutoSuggest>
    {
        internal What3WordsV3 api;
        internal AutosuggestOptions options;
        internal string input;

        public AutosuggestRequest(What3WordsV3 api, string input, AutosuggestOptions options)
        {
            this.api = api;
            this.options = options;
            this.input = input;
        }

        internal override Task<AutoSuggest> ApiRequest
        {
            get
            {
                return api.request.AutoSuggest(input, options);
            }
        }
    }
}