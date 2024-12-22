
namespace advent_of_code.december_1;

internal class Challenge1
{
    public static Data GetData()
    {
        Data data = new Data();
        string dataString = File.ReadAllText("Resources/challenge-1-input.txt");
        var dataArray = dataString.Split('\n');
        data.LeftList = dataArray.Select(s => int.Parse(s.Split(' ')[0])).ToList();
        data.RightList = dataArray.Select(s => int.Parse(s.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1])).ToList();
        return data;
    }

    public static int CalculateDistance(Data data)
    {
        data.LeftList.Sort();
        data.RightList.Sort();

        var sumOfDistances = data.LeftList.Select((x, i) => Math.Abs(data.LeftList[i] - data.RightList[i])).Sum();

        return sumOfDistances;
    }

    public static int CalculateSimilarityScore(Data data)
    {
       return data.LeftList.Select(n => n * data.RightList.Count(x => x == n)).Sum();
    }
}
