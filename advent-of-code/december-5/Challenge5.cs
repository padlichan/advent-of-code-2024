
using System.Data;
using System.Diagnostics;
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
        var correctUpdates = GetCorrectUpdates(updates);
        var sumOfMiddleNumbers = correctUpdates.Select(u => u.Where((n, i) => i == u.Length / 2).FirstOrDefault()).Sum();
        Console.WriteLine("December 5");
        Console.WriteLine($"The sum of middle numbers in correct updates: {sumOfMiddleNumbers}");
        var incorrectUpdates = GetIncorrectUpdates(updates);
        var sortedUpdates = incorrectUpdates.Select(u => SortUpdate(u));
        var sumOfCorrectedMiddleNumbers = sortedUpdates.Select(u => u.Where((n, i) => i == u.Length / 2).FirstOrDefault()).Sum();
        Console.WriteLine($"The sum of middle numbers in corrected updates: {sumOfCorrectedMiddleNumbers}\n");
    }

    private List<int[]> GetCorrectUpdates(List<int[]> updateList)
    {
        return updateList.Where(u => rules.All(r => CheckUpdateForRule(u, r))).ToList();
    }


    private List<int[]> GetIncorrectUpdates(List<int[]> updateList)
    {
        return updateList.Where(u => !rules.All(r => CheckUpdateForRule(u, r))).ToList();
    }

    private int[] SortUpdate(int[] update)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();

        while(GetBrokenRules(update).Length > 0)
        {
            if (timer.ElapsedMilliseconds > 2000) break;
            GetBrokenRules(update).ToList().ForEach(r => SortUpdateForRule(update, r));
        }
        timer.Stop();
        return update;
    }

    private int[] SortUpdateForRule(int[] update, (int, int) rule)
    {
        var indexOfSecond = update.Select((n, i) => (n, i)).Where(p => p.n == rule.Item2).Select(p => p.i).FirstOrDefault(-1);
        if (indexOfSecond == -1) return update;
        var indexOfFirst = update.Select((n, i) => (n, i)).Where(p => p.n == rule.Item1).Select(p => p.i).FirstOrDefault(-1);
        if (indexOfFirst < indexOfSecond) return update;

        var temp = update[indexOfFirst];
        update[indexOfFirst] = update[indexOfSecond];
        update[indexOfSecond] = temp;
        return update;
    }

    private (int, int)[] GetBrokenRules(int[] update)
    {
        return rules.Where(r => !CheckUpdateForRule(update, r)).ToArray();
    }

    private bool CheckUpdateForRule(int[] update, (int, int) rule)
    {
        var indexOfSecond = update.Select((n, i) => (n, i)).Where(p => p.n == rule.Item2).Select(p => p.i).FirstOrDefault(-1);
        if (indexOfSecond == -1) return true;
        var indexOfFirst = update.Select((n, i) => (n, i)).Where(p => p.n == rule.Item1).Select(p => p.i).FirstOrDefault(-1);
        if (indexOfFirst < indexOfSecond) return true;
        return false;
    }

    private (int, int)[] getRules()
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
