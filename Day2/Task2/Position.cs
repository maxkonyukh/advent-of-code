public class Position
{
    public int Horizontal { get; set; }
    public int Depth { get; set; }
    public int Aim { get; set; }

    public Position(int horizontal, int depth, int aim)
    {
        Horizontal = horizontal;
        Depth = depth;
        Aim = aim;
    }
}