using System.Diagnostics;
using System.Text.RegularExpressions;

List<Race> races = [];
Stopwatch watch = new Stopwatch();

void LoadInput() {
    Regex digitMatch = new(@"\d+");
    string[] input = File.ReadAllLines(@"C:\Users\DeltaDizzy\Documents\DOTNET\Advent of Code\2023\Day6-WaitForIt\input.txt");
    int[] times = digitMatch.Matches(input[0]).Select(match => int.Parse(match.Value)).ToArray();
    int[] distances = digitMatch.Matches(input[1]).Select(match => int.Parse(match.Value)).ToArray();
    for (int i = 0; i < times.Count(); i++)
    {
        races.Add(new Race(times[i], distances[i]));
    }
}

int NumberOfWaysToWin(Race race) {
    int availableTime = race.time;
    int distanceToBeat = race.distance;

    // brute force it because why not, no copies are being made so we good
    var buttonHoldTimes = Enumerable.Range(0, availableTime + 1);
    List<Race> possibleRaces = [];
    foreach (int buttonHoldTime in buttonHoldTimes)
    {
        int timeForMoving = availableTime - buttonHoldTime;
        int distanceAchieved = timeForMoving * buttonHoldTime;
        possibleRaces.Add(new Race(timeForMoving, distanceAchieved));
    }
    return possibleRaces.Where(myRace => myRace.distance > race.distance).Count();
}
watch.Start();
LoadInput();
List<int> waysToWin = [];
foreach (Race race in races)
{
    waysToWin.Add(NumberOfWaysToWin(race));
}
watch.Stop();
Console.WriteLine(waysToWin.Aggregate((a, x) => a * x));
Console.WriteLine($"Completed in {watch.ElapsedMilliseconds}ms");

record struct Race(int time, int distance);