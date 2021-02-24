namespace what3words.dotnet.wrapper.response
{
    public class ConvertTo3WA : Response {
        public string Country { get; set; }
        public Square Square { get; set; }
        public string NearestPlace { get; set; }
        public Coordinates Coordinates { get; set; }
        public string Words { get; set; }
        public string Language { get; set; }
        public string Map { get; set; }
    }
}