using System.Diagnostics;
using System.Text.RegularExpressions;

Regex numberMatch = new Regex(@"(\d+)");
// card, # of instances
Dictionary<int, IEnumerable<int>> originalCards = [];
List<int> cardInstances = [];
Stopwatch watch = new Stopwatch();

void LoadInput() {
    string[] lines = File.ReadAllLines(@"C:\Users\DeltaDizzy\Documents\DOTNET\Advent of Code\2023\Day4-Scratchcards\input.txt");
    foreach (string line in lines)
    {
        if (lines.Length <= 7)
        {
            var matches = numberMatch.Matches(line);
            var cardNumbers = matches.Skip(1).Take(5).Select(match => int.Parse(match.Value));
            var winningNumbers = matches.Skip(6).Select(match => int.Parse(match.Value));
            int cardId = int.Parse(matches.Take(1).FirstOrDefault().Value);
            int numberMatched = winningNumbers.Intersect(cardNumbers).Count();
            var cardsToGenerate = Enumerable.Range(Math.Clamp(cardId + 1, 1, 6), numberMatched).Distinct();
            originalCards.Add(cardId, cardsToGenerate);
            cardInstances.Add(cardId);
        } else {
            var matches = numberMatch.Matches(line);
            var cardNumbers = matches.Skip(1).Take(10).Select(match => int.Parse(match.Value));
            var winningNumbers = matches.Skip(11).Select(match => int.Parse(match.Value));
            int cardId = int.Parse(matches.Take(1).FirstOrDefault().Value);
            int numberMatched = winningNumbers.Intersect(cardNumbers).Count();
            var cardsToGenerate = Enumerable.Range(Math.Clamp(cardId + 1, 1, 196), numberMatched).Distinct();
            originalCards.Add(cardId, cardsToGenerate);
            cardInstances.Add(cardId);
        }
    }
}

void CalculateCardInstancesBruteForce() {
// dict of card numbers and card numbers they generate
// list of card numbers
// use dict to fill out list
//  to do this, set all spots in list to 1
//  for each item in the list, addrange [generated card numbers]
// sum list

    
    for (int i = 0; i < cardInstances.Count; i++)
    {
        watch.Start();
        cardInstances.AddRange(originalCards[cardInstances[i]]);
        cardInstances = cardInstances.OrderBy(number => number).ToList();
        Console.WriteLine($"Processing number {cardInstances[i]} in {watch.ElapsedMilliseconds}ms");
        watch.Stop();
        watch.Reset();
    }
    watch.Stop();
    // get sum of card numbers
    var cardTallies =  cardInstances.GroupBy(num => num).Select(group => group.Count());
    Console.WriteLine($"Total Number of Cards: {cardTallies.Sum()}");
    Console.WriteLine($"Ran in {watch.ElapsedMilliseconds}ms");
}

void CalculateCardInstances() {
    // dict of card numbers and card numbers they generate
    // list of numbers of instances of cards
    // use dict to fill out list
    //  to do this, set all spots in list to 1
    cardInstances = Enumerable.Repeat(1, originalCards.Count).ToList();
    //  for each item in the list, add 1 * (num of cards with current id) to each generated slot
    // get cards to generate copies for
    foreach (var item in originalCards)
    {
        var cardsToGenerate = item.Value;
        foreach (int card in cardsToGenerate)
        {
            // card is 2 so slot is 1
            cardInstances[card-1] += 1;
        }
    }
    // get sum of card numbers
    Console.WriteLine($"Total Number of Cards: {cardInstances.Sum()}");
    Console.WriteLine($"Ran in {watch.ElapsedMilliseconds}ms");
}
LoadInput();
watch.Start();
CalculateCardInstances();