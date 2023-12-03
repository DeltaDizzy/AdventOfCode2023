using System.Diagnostics;
using System.Text.RegularExpressions;

Regex symbolMatch = new(@"[/#\-*$+&=%@]");
Regex wordMatch = new(@"[0-9]+");
List<Word> allNumbers = [];
List<Word> partNumbers = [];
List<Symbol> symbols = [];
IEnumerable<Symbol> eligibleSymbols = [];
int width = 0;
int height = 0;
Stopwatch timer = new();

void LoadInput() {
    string[] input = File.ReadAllLines(@"C:\Users\DeltaDizzy\Documents\DOTNET\Advent of Code\2023\Day3-GearRatios\input.txt");
    width = input[0].Length;
    height = input.Length;

    
    for (int i = 0; i < input.Length; i++)
    {   // for each line
        // load words
        foreach (Match match in wordMatch.Matches(input[i])) 
        {
            // for each word on this line
            Position wordPosition = new Position(match.Index, i);
            allNumbers.Add(new Word(wordPosition, match.Value.Length, int.Parse(match.Value)));
        }

        // load symbols
        foreach (Match match in symbolMatch.Matches(input[i]))
        {
            // for each word on this line
            symbols.Add(new Symbol(new Position(match.Index, i), match.Value));
        }
    }

}

bool IsAdjacent(Symbol symbol, Word word) {
    // if symbol is in line horizontally
    int leftLimit = word.StartPosition.X - 1;
    int rightLimit = word.StartPosition.X + word.Length; // adding the length already adds one (it counts the start position)
    int upLimit = word.StartPosition.Y - 1;
    int downLimit = word.StartPosition.Y + 1;
    if (symbol.Position.X >= leftLimit && symbol.Position.X <= rightLimit)
    {
        // if it is in line vertically
        if (symbol.Position.Y >= upLimit && symbol.Position.Y <= downLimit)
        {
            return true;
        }
    }
    return false;
}

void Part1() {
    LoadInput();
    foreach (Word word in allNumbers)
    {
        eligibleSymbols = symbols.Where(symbol => Math.Abs(symbol.Position.Y - word.StartPosition.Y) <= 1);
        Console.WriteLine($"Checking adjacency for word at {word.StartPosition}, {eligibleSymbols.Count()} symbols");
        timer.Start();
        foreach (Symbol symbol in symbols)
        {
            
            
            if (IsAdjacent(symbol, word))
            {
                if (!partNumbers.Contains(word))
                {
                    partNumbers.Add(word);
                }
            }
        }
        Console.WriteLine($"Took {timer.ElapsedMilliseconds}ms");
        timer.Restart();
    }
    Console.WriteLine(partNumbers.Sum(word => word.Text));
    foreach (Word word in allNumbers.Except(partNumbers))
    {
        Console.WriteLine(word);
    }
}

Part1();

record Position(int X, int Y);

record Symbol(Position Position, string Text);

record Word(Position StartPosition, int Length, int Text);
