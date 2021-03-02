namespace what3words.dotnet.wrapper.models
{
    public class Address : Suggestion
    {
        public Coordinates Coordinates { get; set; }
        public string Map { get; set; }
        public Square Square { get; set; }
    }

}