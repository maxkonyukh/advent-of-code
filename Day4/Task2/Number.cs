public class Number
{
    public int Value { get; set; }
    public bool Marked { get; set; } = false;

    public Number(int value)
    {
        Value = value;
    }
}