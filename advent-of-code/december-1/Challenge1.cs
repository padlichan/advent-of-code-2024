
using System.Reflection.Metadata.Ecma335;

namespace advent_of_code.december_1
{
    internal class Challenge1
    {
        public static Data GetData()
        {
            Data data = new Data();
            string dataString = File.ReadAllText("Resources/challenge-1-input.txt");
            var dataArray = dataString.Split('\n');
            data.leftList = dataArray.Select(s => int.Parse(s.Split(' ')[0])).ToList();
            data.rightList = dataArray.Select(s => int.Parse(s.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1])).ToList();
            return data;
        }

        public int ProcessData(Data data)
        {
            data.leftList.Sort();
            data.rightList.Sort();

            var sumOfDistances = data.leftList.Select((x, i) => Math.Abs(data.leftList[i] - data.rightList[i])).Sum();

            return sumOfDistances;

        }
    }

    public class Data
    {
        public List<int> leftList = [];
        public List<int> rightList = [];
    }
}
