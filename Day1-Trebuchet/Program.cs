using System.Text.RegularExpressions;

string[] rawLines = File.ReadAllLines(@"C:\Users\DeltaDizzy\Documents\DOTNET\Advent of Code\2023\Day1-Trebuchet\input.txt");
Regex digitFinder = new Regex(@"(?=(\d{1}|one|two|three|four|five|six|seven|eight|nine))");
int sum = 0;
foreach (string line in rawLines)
{
    var matches = digitFinder.Matches(line);
    string firstDigit = ConvertToNumeric(matches[0].Groups[1].Value);
    string lastDigit = ConvertToNumeric(matches[^1].Groups[1].Value);
    sum += int.Parse($"{firstDigit}{lastDigit}");
}
Console.WriteLine(sum);

string ConvertToNumeric(string digit) =>
    digit switch {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",
        "nine" => "9",
        _ => digit
    };