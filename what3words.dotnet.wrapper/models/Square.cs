namespace what3words.dotnet.wrapper.models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Square
    {
        public Square()
        {
        }

        public Square(Coordinates sw, Coordinates ne)
        {
            Southwest = sw;
            Northeast = ne;
        }

        public Coordinates Southwest { get; set; }
        public Coordinates Northeast { get; set; }
    }
}
