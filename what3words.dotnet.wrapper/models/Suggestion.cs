namespace what3words.dotnet.wrapper.models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Suggestion
    {
        public string Country { get; set; }
        public string NearestPlace { get; set; }
        public string Words { get; set; }
        public string Language { get; set; }
        public int DistanceToFocusKm { get; set; }
        public int Rank { get; set; }
    }


    public class SuggestionWithCoordinates : Suggestion
    {
        public Coordinates Coordinates { get; set; }
    }
}