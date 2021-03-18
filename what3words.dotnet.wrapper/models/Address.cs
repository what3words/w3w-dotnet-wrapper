namespace what3words.dotnet.wrapper.models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Address
    {
        public Coordinates Coordinates { get; set; }
        public string Map { get; set; }
        public Square Square { get; set; }
        public string Country { get; set; }
        public string NearestPlace { get; set; }
        public string Words { get; set; }
        public string Language { get; set; }
    }

}