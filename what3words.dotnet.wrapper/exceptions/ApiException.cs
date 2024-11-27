using System;

namespace what3words.dotnet.wrapper.exceptions
{
    internal class ApiException<T> : Exception
    {
        public readonly T Error;

        public ApiException(string message, T error) : base(message)
        {
            Error = error;
        }
    }
}
