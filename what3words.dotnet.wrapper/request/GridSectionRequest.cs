using System;
using System.Threading.Tasks;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;

namespace what3words.dotnet.wrapper.request
{
    public class GridSectionRequest
    {
        private What3WordsV3 api;
        private string boundingBox;

        public GridSectionRequest(What3WordsV3 api, Square boundingBox)
        {
            this.api = api;
            this.boundingBox = boundingBox.Southwest.Lat + "," + boundingBox.Southwest.Lng + "," +
                     boundingBox.Northeast.Lat + "," + boundingBox.Northeast.Lng;
        }

        public async Task<APIResponse<GridSection>> RequestAsync()
        {
            try
            {
                return new APIResponse<GridSection>(await api.request.GridSection(boundingBox));
            }
            catch (Refit.ApiException e)
            {
                var apiException = await e.GetContentAsAsync<ApiException>();
                return new APIResponse<GridSection>(apiException.Error);
            }
            catch (Exception e)
            {
                var error = new APIError();
                error.Code = What3WordsError.NetworkError.ToString();
                error.Message = e.Message;
                return new APIResponse<GridSection>(error);
            }
        }
    }
}
