using System.Text;
using System.Text.RegularExpressions;

string[] rawLines = File.ReadAllLines(@"C:\Users\DeltaDizzy\Documents\DOTNET\Advent of Code\2023\Day1-Trebuchet\input.txt");
Regex digitFinder = new Regex("\\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)");
int sum = 0;
foreach (string line in rawLines)
{
    var replacedLine = digitFinder.Replace(line.ToLower(), MakeNumeric);
    var digits = replacedLine.ToCharArray().Where(IsDigit).Select(c => c);
    sum += int.Parse($"{digits.First()}{digits.Last()}");
}
Console.WriteLine(sum);

bool IsDigit(char c) {
    return char.IsDigit(c);
}

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

string MakeNumeric(Match match) {
    return ConvertToNumeric(match.Value);
}
/*string GetArrayAsString(string[] array) {
    StringBuilder stringBuilder = new();
    stringBuilder.Append("[ ");
    foreach (string item in array)
    {
        stringBuilder.Append($"{item} ");
    }
    stringBuilder.Append("]");
    return stringBuilder.ToString();
}

struct CalibrationValue(string digits, string leftNum, string rightNum) {

    public override string ToString()
    {
        return $"CalibrationValue {{ digits = {digits}, leftNum = {leftNum}, rightNum = {rightNum} }}";
    }

    public int Value {
        get { return int.Parse(leftNum + rightNum); }
    }
}*/

