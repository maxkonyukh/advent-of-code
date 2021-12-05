var currentDirectory = Directory.GetCurrentDirectory();
string[] lines = File.ReadAllLines(Path.Combine(currentDirectory, "input.txt"));

var numbers = lines
    .FirstOrDefault()!
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(value => int.Parse(value))
    .ToList();

var cardLines = lines
    .Skip(2)
    .ToList();

var cards = new List<Card>();
int row = 0;
var card = new Card(5, 5);

foreach (var line in cardLines)
{
    var lineNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (lineNumbers.Any())
    {
        for (int i = 0; i < lineNumbers.Length; i++)
            card[row, i] = new Number(int.Parse(lineNumbers[i]));
    
        row++;

        if (row % 5 == 0)
        {
            cards.Add(card);
            row = 0;
            card = new Card(5, 5);
        }
    }
}

var (defeatCard, lastNumber) = GetDefeatCard(numbers, cards);
if (defeatCard is { })
{
    DisplayResult(new List<Card> { defeatCard });
    var sumOfUnmarkedItems = CalculateSumOfUnmarkedNumbers(defeatCard);
    Console.WriteLine($"Loosing number: {lastNumber}");
    Console.WriteLine($"Sum of unmarked items: {sumOfUnmarkedItems}");
    Console.WriteLine($"Final result: {lastNumber * sumOfUnmarkedItems}");
}

int CalculateSumOfUnmarkedNumbers(Card card)
{
    var sum = 0;
    for (int i = 0; i < card.Rows; i++)
    {
        for (int j = 0; j < card.Columns; j++)
        {
            if (!card[i, j].Marked)
                sum += card[i, j].Value;
        }
    }

    return sum;
}

(Card?, int?) GetDefeatCard(IEnumerable<int> numbers, IEnumerable<Card> cards)
{
    var filledCards = new HashSet<Card>();
    foreach (var number in numbers)
    {
        foreach (var filledCard in cards)
        {
            for (int i = 0; i < filledCard.Rows; i++)
            {
                for (int j = 0; j < filledCard.Columns; j++)
                {
                    if (filledCard[i, j].Value == number)
                        filledCard[i, j].Marked = true;
                }
            }
        }

        foreach (var filledCard in cards)
        {
            for (int i = 0; i < filledCard.Rows; i++)
            {
                var collumns = 0;
                for (int j = 0; j < filledCard.Columns; j++)
                {
                    if (filledCard[i, j].Marked)
                        collumns++;
                }
                if (collumns == filledCard.Columns)
                    filledCards.Add(filledCard);
            }

            for (int i = 0; i < filledCard.Rows; i++)
            {
                var rows = 0;
                for (int j = 0; j < filledCard.Columns; j++)
                {
                    if (filledCard[j, i].Marked)
                        rows++;
                }
                if (rows == filledCard.Rows)
                    filledCards.Add(filledCard);
            }

            if (filledCards.Count() == cards.Count())
                return (filledCards.LastOrDefault(), number);
        }
    }

    return (null, null);
}

void DisplayResult(IEnumerable<Card> cards)
{
    var consoleColor = Console.ForegroundColor;
    foreach (var filledCard in cards)
    {
        for (int i = 0; i < filledCard.Rows; i++)
        {
            for (int j = 0; j < filledCard.Columns; j++)
            {
                if (filledCard[i, j].Marked)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.Write($"{filledCard[i, j].Value} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
