namespace what3words.dotnet.wrapper.response
{
    public class ApiException
    {
        public APIError Error { get;set; }
    }

    public class APIResponse<T>
    {
        public APIResponse(T data)
        {
            this.Data = data;
        }

        public APIResponse(APIError error)
        {
            this.Error = error;
        }

        public T Data { get; private set; }
        public APIError Error { get; private set; }

        public bool IsSuccessful { get { return Data != null; } }
    }
}