using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalcKata1
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            var sum = 0;
            var delimiters = new List<string> {",", "\n"};

            if (string.IsNullOrWhiteSpace(numbers)) return sum;

            if (numbers.StartsWith("//"))
            {
                delimiters.AddRange(FindDelimiters(numbers));
                numbers = RemoveDelimIndicatorStrings(numbers, new List<string>{"//", "[", "]"});
            }

            var integers = numbers.Split(delimiters.ToArray(),
                StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s));

            var negativeList = integers.Where(i => i < 0);

            if (negativeList.Any())
            {
                throw new ArgumentException($"Negatives not allowed: {string.Join(",", negativeList.ToArray())}");
            }

            return integers
                .Where(i => i <= 1000)
                .Sum();
        }

        private string RemoveDelimIndicatorStrings(string numbers, List<string> delimIndicatorList)
        {
            foreach (var delimiterIndicator in delimIndicatorList)
            {
                numbers = numbers.Replace(delimiterIndicator, "");
            }

            return numbers;
        }

        private List<string> FindDelimiters(string numbers)
        {
            var results = new List<string>();

            if (numbers.Contains("[") && numbers.Contains("]"))
            {
                var matches = Regex.Matches(numbers, @"\[(.*?)\]");
                foreach (Match match in matches)
                {
                    results.Add(match.Groups[1].Value);
                }
            }
            else
            {
                results.Add(numbers.Substring(2, 1));
            }

            return results;
        }
    }
}
