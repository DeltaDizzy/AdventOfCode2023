using System.Diagnostics;
using System.Text.RegularExpressions;

Regex numberMatch = new Regex(@"(\d+)");
List<Card> cards = [];
Stopwatch watch = new Stopwatch();

void LoadInput() {
    string[] lines = File.ReadAllLines(@"C:\Users\DeltaDizzy\Documents\DOTNET\Advent of Code\2023\Day4-Scratchcards\input.txt");
    foreach (string line in lines)
    {
        var matches = numberMatch.Matches(line);
        var cardNumbers = matches.Skip(1).Take(10).Select(match => int.Parse(match.Value));
        var winningNumbers = matches.Skip(10).Select(match => int.Parse(match.Value));
        cards.Add(new Card(int.Parse(matches.Take(1).FirstOrDefault().Value), cardNumbers, winningNumbers));
    }
}

LoadInput();
int maxNumber = 0;
for (int i = 0; i < cards.Count; i++)
{
    watch.Start();
    Card card = cards[i];
    int numbersWon = GetNumberMatches(card);
    if (numbersWon is 0) continue;
    // get next [numbersWon] cards
    // get card numbers to copy
    var numbersToCopy = Enumerable.Range(card.Number + 1, numbersWon).ToList();
    maxNumber = Math.Max(maxNumber, numbersToCopy.Max());
    // get cards to copy
    List<Card> cardsToCopy = [];
    foreach (int numberToCopy in numbersToCopy)
    {
        Card? cardToCopy = cards.Find(c => c.Number == numberToCopy);
        if (cardToCopy is not null)
        {
            cardsToCopy.Add(cardToCopy);
        } else {
            Console.WriteLine($"i is {i}");
            Console.WriteLine($"numberToCopy is {numberToCopy}");
            throw new ArgumentNullException("cardToCopy");
        }
        
    }
    // copy cards into main list
    cards.AddRange(cardsToCopy);
    //resort list
    try
    {
        cards = cards.OrderBy(c => c.Number).ToList();
    }
    catch (NullReferenceException nre)
    {
        Console.WriteLine(nre.Message);
        Console.WriteLine(nre.StackTrace);
        Console.WriteLine(nre.InnerException);
        Console.WriteLine($"i is {i}");
    }
    watch.Stop();
    Console.WriteLine($"Card {card.Number} at index {i} out of {cards.Count} took {watch.ElapsedMilliseconds}ms");
    Console.WriteLine($"The largest card number copied so far is {maxNumber}");
    watch.Reset();
}
Console.WriteLine($"We have {cards.Count} cards.");
Console.WriteLine($"Numbers of each Card Number:");
foreach (Card card in cards.DistinctBy(c => c.Number))
{
    Console.WriteLine($"{card.Number}: {cards.Where(c => c.Number == card.Number).Count()}");
}

int GetNumberMatches(Card card) {
    List<int> matches = card.CardNumbers.Intersect(card.WinningNumbers).ToList();
    //Console.WriteLine($"Card {card.Number} Matching Numbers:");
    //matches.ForEach(Console.WriteLine);
    return matches.Count();
}

record Card(int Number, IEnumerable<int> CardNumbers, IEnumerable<int> WinningNumbers);