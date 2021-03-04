namespace what3words.dotnet.wrapper.models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Coordinates
    {
        public Coordinates()
        {
        }

        public Coordinates(double lat, double lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }

        public double Lng { get; set; }
        public double Lat { get; set; }
    }
}