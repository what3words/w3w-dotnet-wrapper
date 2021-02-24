using System;

namespace what3words.dotnet.wrapper.response
{
    public class APIError
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public What3WordsError Error { get
            {
                var error = What3WordsError.UnknownError;
                Enum.TryParse(Code, out error);
                return error;
            }
        }
    }
}