namespace what3words.dotnet.wrapper.models
{
    public class Suggestion {
        public string Country { get; set; }
        public string NearestPlace { get; set; }
        public string Words { get; set; }
        public string Language { get; set; }

        public int? DistanceToFocusKm { get; set; }
        public int? Rank { get; set; }
    }

}