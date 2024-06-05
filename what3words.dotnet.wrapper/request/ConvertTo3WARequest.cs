using Refit;
using System.Globalization;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConvertTo3WARequest : Request<Address>
    {
        public class ConvertTo3WAOptions
        {
            [AliasAs("coordinates")]
            public string Coordinates { get; set; }
            [AliasAs("language")]
            public string Language { get; set; }
        }

        private readonly What3WordsV3 _api;
        private readonly ConvertTo3WAOptions _options;

        public ConvertTo3WARequest(What3WordsV3 api, Coordinates coordinates)
        {
            _api = api;
            _options = new ConvertTo3WAOptions
            {
                Coordinates = coordinates.Lat.ToString(CultureInfo.InvariantCulture) + "," + coordinates.Lng.ToString(CultureInfo.InvariantCulture)
            };
        }

        public ConvertTo3WARequest Language(string language)
        {
            _options.Language = language;
            return this;
        }

        protected override Task<Address> ApiRequest
        {
            get
            {
                return _api.Request.ConvertTo3WA(_options);
            }
        }
    }
}
