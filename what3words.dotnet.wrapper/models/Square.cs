namespace what3words.dotnet.wrapper.models
{
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
