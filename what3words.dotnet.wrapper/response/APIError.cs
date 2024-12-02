using System;

namespace what3words.dotnet.wrapper.response
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class APIError
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public What3WordsError Error
        {
            get
            {
                _ = Enum.TryParse(Code, out What3WordsError error);
                return error;
            }
        }
    }
}