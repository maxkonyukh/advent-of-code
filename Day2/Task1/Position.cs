public class Position
{
    public int Horizontal { get; set; }
    public int Depth { get; set; }

    public Position(int horizontal, int depth)
    {
        Horizontal = horizontal;
        Depth = depth;
    }
}