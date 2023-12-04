using System.Text.RegularExpressions;

Regex numberMatch = new Regex(@"(\d+)");
List<Card> cards = [];

void LoadInput() {
    string[] lines = File.ReadAllLines(@"C:\Users\DeltaDizzy\Documents\DOTNET\Advent of Code\2023\Day4-Scratchcards\input.txt");
    foreach (string line in lines)
    {
        var matches = numberMatch.Matches(line).Skip(1);
        var cardNumbers = matches.Take(10).Select(match => int.Parse(match.Value));
        var winningNumbers = matches.Skip(10).Select(match => int.Parse(match.Value));
        cards.Add(new Card(cardNumbers, winningNumbers));
    }
}

LoadInput();
foreach (Card card in cards)
{
    int numbersWon = GetNumberMatches(card);
    if (numbersWon is 0) continue;
    card.Points = (int)Math.Pow(2, numbersWon - 1);
}
Console.WriteLine(cards.Sum(card => card.Points));

int GetNumberMatches(Card card) {
    return card.CardNumbers.Intersect(card.WinningNumbers).Count();
}

record Card(IEnumerable<int> CardNumbers, IEnumerable<int> WinningNumbers) {
    public int Points { get; set; } = 0;
};