using System.Text.RegularExpressions;


namespace advent_of_code.december_3
{
    internal class Challenge3
    {
        private string path = "Resources/challenge-3-input.txt";
        public Challenge3()
        {
            string data = GetData();

            //Part 1
            var muls = GetMuls(data);
            var pairs = GetPairs(muls);
            var result = pairs.Select(p => p.Item1 * p.Item2).Sum();
            Console.WriteLine("December 3");
            Console.WriteLine($"The sum of multiplied pairs: {result}");

            //Part 2
            var mulsAndConditionals = GetMulsAndConditionals(data);
            var conditionalPairs = GetConditionalPairs(mulsAndConditionals);
            var result2 = conditionalPairs.Select(cp => cp.Item1 * cp.Item2).Sum();
            Console.WriteLine($"The sum of conditional multiplied pairs: {result2}\n");
        }

        private List<(int,int)> GetPairs(List<string> muls)
        {
            var output = muls.Select(m => GetToupleFromMul(m)).ToList();
            return output;
        }

        private (int,int) GetToupleFromMul(string mul)
        {
            var numArray = mul.TrimStart("mul(".ToCharArray())
               .TrimEnd(')')
               .Split(",")
               .Select(int.Parse).ToArray();

            return (numArray[0],numArray[1]);
        }

        private List<(int,int)> GetConditionalPairs(List<string> mulsAndConditionals)
        {
            bool isDo = true;
            List<(int,int)> conditionalPairs = [];
            foreach (var mulOrConditional in mulsAndConditionals)
            {
                if(mulOrConditional == "do()") isDo = true;
                else if(mulOrConditional == "don't()") isDo = false;
                else
                {
                    if(isDo) conditionalPairs.Add(GetToupleFromMul(mulOrConditional));                   
                }
            }
            return conditionalPairs;
        }

        List<string> GetMulsAndConditionals(string data)
        {
            string mulPattern = @"mul\(\d{1,3},\d{1,3}\)";
            string DoPattern = @"do\(\)";
            string DontPattern = @"don't\(\)";
            string combinedPattern = $@"({mulPattern})|({DoPattern})|({DontPattern})";
            var mulsAndConditionals = Regex.Matches(data, combinedPattern).Select(mc => mc.Value).ToList();
            return mulsAndConditionals;
        }

        List<string> GetMuls(string data)
        {
            string mulPattern = @"mul\(\d{1,3},\d{1,3}\)";
            var muls = Regex.Matches(data, mulPattern).Select(m => m.Value).ToList();
            return muls;
        }

        private string GetData()
        {
            return File.ReadAllText(path);
        }
    }
}
