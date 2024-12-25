using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace advent_of_code.december_3
{
    internal class Challenge3
    {
        private string path = "Resources/challenge-3-input.txt";
        public Challenge3()
        {
            string data = GetData();
            var muls = GetMuls(data);
            var pairs = GetPairs(muls);
            var result = pairs.Select(p => p.Item1 * p.Item2).Sum();
            Console.WriteLine("December 3");
            Console.WriteLine($"The sum of multiplied pairs: {result}\n");
        }

        private List<(int,int)> GetPairs(List<string> muls)
        {
            var output = muls.Select(m =>
            {
                var result = m.TrimStart("mul(".ToCharArray())
                              .TrimEnd(')')
                              .Split(',')
                              .Select(int.Parse).ToArray();

                return (result[0], result[1]); 
            }).ToList();
            return output;
        }

        List<string> GetMuls(string data)
        {
            string pattern = @"mul\(\d{1,3},\d{1,3}\)";
            var muls = Regex.Matches(data, pattern).Select(m => m.Value).ToList();
            return muls;
        }

        private string GetData()
        {
            return File.ReadAllText(path);
        }
    }
}
