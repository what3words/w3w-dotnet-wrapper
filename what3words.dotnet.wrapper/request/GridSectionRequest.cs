using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;

namespace what3words.dotnet.wrapper.request
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class GridSectionRequest : Request<GridSection>
    {
        private What3WordsV3 api;
        private string boundingBox;

        public GridSectionRequest(What3WordsV3 api, Coordinates southWest, Coordinates northEast)
        {
            this.api = api;
            this.boundingBox = southWest.Lat + "," + southWest.Lng + "," +
                     northEast.Lat + "," + northEast.Lng;
        }

        internal override Task<GridSection> ApiRequest
        {
            get
            {
                return api.request.GridSection(boundingBox);
            }
        }
    }
}
