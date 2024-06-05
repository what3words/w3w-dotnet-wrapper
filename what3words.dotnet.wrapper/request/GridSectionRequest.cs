using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class GridSectionRequest : Request<GridSection>
    {
        private readonly What3WordsV3 _api;
        private readonly string _boundingBox;

        public GridSectionRequest(What3WordsV3 api, Coordinates southWest, Coordinates northEast)
        {
            _api = api;
            _boundingBox = southWest.Lat + "," + southWest.Lng + "," +
                     northEast.Lat + "," + northEast.Lng;
        }

        protected override Task<GridSection> ApiRequest
        {
            get
            {
                return _api.Request.GridSection(_boundingBox);
            }
        }
    }
}
