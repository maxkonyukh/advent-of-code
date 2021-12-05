public class Card
{
    public int Rows { get; }
    public int Columns { get; }

    private Number[,] _numbers;

    public Card(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;

        _numbers = new Number[rows, columns];
    }

    public Number this[int row, int column]
    {
        get { return _numbers[row, column]; }
        set { _numbers[row, column] = value; }
    }
}