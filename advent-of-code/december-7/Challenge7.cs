using System.Reflection.Metadata;

namespace advent_of_code.december_7;

internal class Challenge7
{

    private readonly Func<long, long, long> add = (long a, long b) => a + b;
    private readonly Func<long, long, long> multiply = (long a, long b) => a * b;
    public Challenge7()
    {
        var numbersList = GetNumbersList();
        var results = GetResults();
        long sumOfEquations = results.Where((r, i) => CheckLineForAll(numbersList[i], r)).Sum();
        Console.WriteLine("December 7");
        Console.WriteLine($"Sum of correct equation results: {sumOfEquations}");
    }

    public List<long> GetResults()
    {
        var data = File.ReadAllText("Resources/challenge-7-input.txt");
        var dataArray = data.Split(Environment.NewLine);
        var results = dataArray.Select(line => long.Parse(line.Split(':')[0])).ToList();
        return results;
    }
    public List<List<long>> GetNumbersList()
    {
        var data = File.ReadAllText("Resources/challenge-7-input.txt");
        var dataArray = data.Split(Environment.NewLine);
        var numbers = dataArray.Select(line => line.Split(' ').Skip(1).Select(long.Parse).ToList()).ToList();
        return numbers;
    }

    private List<List<Func<long, long, long>>> GetOperatorsList(long number)
    {
        var limit = ((long)Math.Pow(2, number - 1)) - 1;
        List<string> binaries = [];
        for (int i = 0; i <= limit; i++)
        {
            var binary = Convert.ToString(i, 2).PadLeft(Convert.ToString(limit, 2).Length, '0');
            binaries.Add(binary);
        }

        var operatorsList = binaries.Select(b => b.Select(c => c == '1' ? add : multiply).ToList()).ToList();
        return operatorsList;
    }

    private bool CheckLine(List<long> numbers, long expectedResult, List<Func<long, long, long>> operators)
    {
        var tempNumbers = numbers.Select(x => x).ToList();
        long result = 0;

        for (int i = 0; i < operators.Count; i++)
        {
            result = operators[i](tempNumbers[0], tempNumbers[1]);
            tempNumbers.RemoveAt(0);
            tempNumbers[0] = result;
        }
        return result == expectedResult;
    }

    private bool CheckLineForAll(List<long> numbers, long expectedResult)
    {
        var operatorsList = GetOperatorsList(numbers.Count);
        var result = operatorsList.Any(o => CheckLine(numbers, expectedResult, o));
        return result;
    }

    private void PrintNumbers(List<long> numbers)
    {
        numbers.ForEach(n => Console.Write(n + " "));
        Console.WriteLine();
    }
}
