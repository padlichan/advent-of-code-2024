using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace advent_of_code.december_7;

internal class Challenge7
{

    private readonly Func<long, long, long> add = (long a, long b) => a + b;
    private readonly Func<long, long, long> multiply = (long a, long b) => a * b;
    private readonly Func<long, long, long> concat = (long a, long b) => long.Parse(a.ToString()+b.ToString());
    public Challenge7()
    {
        var dataArray = ReadData("Resources/challenge-7-input.txt");
        var numbersList = GetNumbersData(dataArray);
        var results = GetResultsData(dataArray);
        long sumOfEquations = results.Where((r, i) => CheckLineForAll(numbersList[i], r)).Sum();
        Console.WriteLine("December 7");
        Console.WriteLine($"Sum of correct equation results: {sumOfEquations}");

        long sumOfEquationsWithConcat = results.Where((r, i) => CheckLineForAllConcat(numbersList[i], r)).Sum();
        Console.WriteLine($"Sum of correct equation results with concationation: {sumOfEquationsWithConcat}");
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

    private List<List<Func<long, long, long>>> GetOperatorsListWithConcat(long number)
    {
        int limit = ((int)Math.Pow(3, number - 1)) - 1;
        List<string> binaries = [];
        for (int i = 0; i <= limit; i++)
        {
            var binary = ConvertToBase3(i).PadLeft(ConvertToBase3(limit).Length, '0');
            binaries.Add(binary);
        }

        var operatorsList = binaries.Select(b => b.Select(c => c == '1' ? add : c == '2' ? multiply : concat).ToList()).ToList();
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

    private bool CheckLineForAllConcat(List<long> numbers, long expectedResult)
    {
        var operatorsList = GetOperatorsListWithConcat(numbers.Count);
        var result = operatorsList.Any(o => CheckLine(numbers, expectedResult, o));
        return result;
    }

    private string ConvertToBase3(int number)
    {
        StringBuilder base3 = new StringBuilder();

        while (number > 0)
        {
            int remainder = number % 3;
            base3.Insert(0, remainder); 
            number /= 3;
        }
        return base3.ToString();
    }

    private string[] ReadData(string path)
    {
        var data = File.ReadAllText(path);
        var dataArray = data.Split(Environment.NewLine);
        return dataArray;
    }
    public List<long> GetResultsData(string[] dataArray)
    {
        var results = dataArray.Select(line => long.Parse(line.Split(':')[0])).ToList();
        return results;
    }
    public List<List<long>> GetNumbersData(string[] dataArray)
    {
        var numbers = dataArray.Select(line => line.Split(' ').Skip(1).Select(long.Parse).ToList()).ToList();
        return numbers;
    }
    private void PrintNumbers(List<long> numbers)
    {
        numbers.ForEach(n => Console.Write(n + " "));
        Console.WriteLine();
    }
}
