namespace what3words.dotnet.wrapper.response
{
    public class Response
    {
        public APIError Error { get; set; }

        public bool IsSuccessful { get { return Error == null; } }
    }
}