
namespace advent_of_code.december_2;
internal class Challenge2
{
    public static Data GetData()
    {
        var stringData = File.ReadAllText("Resources/challenge-2-input.txt");
        Data data = new Data();
        var dataArray = stringData.Split('\n');
        data.Reports = dataArray.Select(s => s.Split(' ').Select(int.Parse).ToArray()).ToList();
        return data;
    }

    public static int CountSafeReports(Data data)
    {
        return data.Reports.Count(r => isSafelyIncreasing(r) || isSafelyDecresing(r));
    }

    private static bool isSafelyIncreasing(int[] report)
    {
        return report.Zip(report.Skip(1),(a,b) => a<b && b-a<=3 && b-a>0).All(x=>x);
    }

    private static bool isSafelyDecresing(int[] report)
    {
        return report.Zip(report.Skip(1), (a, b) => a > b && a - b <= 3 && a - b > 0).All(x => x);
    }
}

public class Data
{
    public List<int[]> Reports { get; set; } = [];
}