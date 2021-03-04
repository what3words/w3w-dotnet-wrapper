using Refit;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConvertTo3WARequest : Request<Address> {
        public class ConvertTo3WAParams
        {
            [AliasAs("coordinates")]
            public string Coordinates { get; set; }
            [AliasAs("language")]
            public string Language { get; set; }
        }

        private What3WordsV3 api;
        private ConvertTo3WAParams queryParams;

        public ConvertTo3WARequest(What3WordsV3 api, Coordinates coordinates)
        {
            this.api = api;
            queryParams = new ConvertTo3WAParams();
            queryParams.Coordinates = coordinates.Lat + "," + coordinates.Lng;
        }

        public ConvertTo3WARequest Language(string language)
        {
            queryParams.Language = language;
            return this;
        }

        internal override Task<Address> ApiRequest
        {
            get
            {
                return api.request.ConvertTo3WA(queryParams);
            }
        }
    }
}
