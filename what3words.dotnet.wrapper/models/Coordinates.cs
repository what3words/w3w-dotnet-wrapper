namespace what3words.dotnet.wrapper.models
{
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