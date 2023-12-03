using System.Text;
using System.Text.RegularExpressions;
Regex gameIdMatch = new Regex(@"(?<!Game)\d+(?=:)");
Regex redMatch = new Regex(@"\d+(?= red)");
Regex greenMatch = new Regex(@"\d+(?= green)");
Regex blueMatch = new Regex(@"\d+(?= blue)");
List<GameData> allGames = new();

GameData ParseGameData(string toParse) {
    int gameId = int.Parse(gameIdMatch.Match(toParse).Value);
    return new GameData(gameId, ParseGameRounds(toParse));
}

List<GameRound> ParseGameRounds(string toParse) {
    string[] rounds = toParse.Split(';');
    GameRound[] gameRounds = new GameRound[rounds.Length];
    for (int i = 0; i < rounds.Length; i++)
    {   
        string match = redMatch.Match(rounds[i]).Value;
        int redNumber = int.Parse(match != "" ? match : "0");
        match = greenMatch.Match(rounds[i]).Value;
        int greenNumber = int.Parse(match != "" ? match : "0");
        match = blueMatch.Match(rounds[i]).Value;
        int blueNumber = int.Parse(match != "" ? match : "0");
        gameRounds[i] = new GameRound(redNumber, greenNumber, blueNumber);
    }
    return gameRounds.ToList();
}

void LoadInput() {
    // Load input
    string[] gameText = File.ReadAllLines(@"C:\Users\DeltaDizzy\Documents\DOTNET\Advent of Code\2023\Day2-CubeConundrum\input.txt");
    gameText.ToList().ForEach(s => allGames.Add(ParseGameData(s)));
    Console.WriteLine("Games loaded");
}

void Part1() {
    LoadInput();

    // define max numbers
    GameRound max = new GameRound(12, 13, 14);

    // find possible games
    List<GameData> possibleGames = allGames
        .Where(game => game.rounds.Max(round => round.red) <= max.red)
        .Where(game => game.rounds.Max(round => round.green) <= max.green)
        .Where(game => game.rounds.Max(round => round.blue) <= max.blue)
        .ToList();

    //sum IDs
    int possibleIdSum = possibleGames.Sum(game => game.Id);
    Console.WriteLine(possibleIdSum);
}

void Part2() {
    LoadInput();
    // find required cube counts for each game
    IEnumerable<GameRound> neededCubes = allGames.Select(game => game.MaxValues);
    // find power for each game
    IEnumerable<int> powers = neededCubes.Select(game => game.red * game.green * game.blue);
    // sum powers
    int sumOfPowers = powers.Sum();
    Console.WriteLine(sumOfPowers);
}

Part2();

record GameRound(int red, int green, int blue);
record GameData(int Id, List<GameRound> rounds) {
    public GameRound MaxValues => new GameRound(rounds.Max(round => round.red), rounds.Max(round => round.green), rounds.Max(round => round.blue));
}