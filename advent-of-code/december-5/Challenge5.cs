
using System.Reflection.Metadata.Ecma335;

namespace advent_of_code.december_5;

internal class Challenge5
{
    private readonly string rulesPath = "Resources/challenge-5-input-rules.txt";
    private readonly string updatesPath = "Resources/challenge-5-input-updates.txt";
    private (int, int)[] rules;
    private List<int[]> updates;
    public Challenge5()
    {
        updates = getUpdates();
        rules = getRules();
        var correctUpdates = FilterUpdates(updates);
        var sumOfMiddleNumbers = correctUpdates.Select(u => u.Where((n, i) => i == u.Length / 2).FirstOrDefault()).Sum();
        Console.WriteLine("December 5");
        Console.WriteLine($"The sum of middle numbers in correct updates: {sumOfMiddleNumbers}");
    }

    private List<int[]> FilterUpdates(List<int[]> updateList)
    {
        return updateList.Where(u => rules.All(r => CheckUpdateForRule(r, u))).ToList();
    }

    private bool CheckUpdateForRule ((int,int) rule, int[] update)
    {
        var indexOfSecond = update.Select((n,i) => (n,i)).Where(p => p.n == rule.Item2).Select(p => p.i).FirstOrDefault(-1);
        if (indexOfSecond == -1) return true;
        var indexOfFirst = update.Select((n, i) => (n, i)).Where(p => p.n == rule.Item1).Select(p => p.i).FirstOrDefault(-1);
        if(indexOfFirst < indexOfSecond) return true;
        return false;
    }

    private (int,int)[] getRules()
    {
        string data = File.ReadAllText(rulesPath);
        return data.Split(Environment.NewLine).Select(line => (int.Parse(line.Split('|')[0]), int.Parse(line.Split('|')[1]))).ToArray();
    }

    private List<int[]> getUpdates()
    {
        string[] data = File.ReadAllText(updatesPath).Split(Environment.NewLine);
        return data.Select(line => line.Split(',').Select(int.Parse).ToArray()).ToList();
    }
}
