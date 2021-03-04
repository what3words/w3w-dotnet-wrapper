namespace what3words.dotnet.wrapper.response
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum What3WordsError
    {
        BadCoordinates,
        BadLanguage,
        BadWords,
        BadInput,
        BadNResults,
        BadNFocusResults,
        BadFocus,
        BadClipToCircle,
        BadClipToBoundingBox,
        BadClipToCountry,
        BadClipToPolygon,
        BadInputType,
        BadBoundingBox,
        BadBoundingBoxTooBig,
        InternalServerError,
        InvalidKey,
        SuspendedKey,
        UnknownError,
        NetworkError,
        InvalidApiVersion,
        InvalidReferrer,
        InvalidIpAddress,
        InvalidAppCredential
    }
}