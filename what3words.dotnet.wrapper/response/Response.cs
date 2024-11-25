using Newtonsoft.Json;

namespace what3words.dotnet.wrapper.response
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class APIResponse<T>
    {
        public T Data { get; private set; }
        [JsonProperty("error")]
        public APIError Error { get; private set; }

        public APIResponse(T data)
        {
            Data = data;
        }

        public APIResponse(APIError error)
        {
            Error = error;
        }

        public bool IsSuccessful => Data != null;
    }

    public class APIResponse
    {
        [JsonProperty("error")]
        public APIError Error { get; private set; }

        public APIResponse()
        {
        }

        public APIResponse(APIError error)
        {
            Error = error;
        }


        public bool IsSuccessful => Error == null;
    }
}