namespace what3words.dotnet.wrapper.models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Address : Suggestion
    {
        public Coordinates Coordinates { get; set; }
        public string Map { get; set; }
        public Square Square { get; set; }
    }

}