using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var text1 = System.IO.File.ReadAllText(@"input-1.txt");
            var input1 = text1.Split(Environment.NewLine).Select(i => Convert.ToInt32(i)).ToList();
            var output1 = Test1(input1);
            Console.WriteLine($"Result of test1 is : {output1}");

            var text2 = System.IO.File.ReadAllText(@"input-2.txt");
            var input2 = text2.Split(Environment.NewLine).Select(i => Convert.ToInt32(i)).ToList();
            var output2 = Test2(input2);
            Console.WriteLine($"Result of test2 is : {output2}");
        }

        private static int Test1(List<int> input)
        {
            var count = 0;
            foreach(var i in Enumerable.Range(1,input.Count-1))
            {
                if (input[i - 1] < input[i])
                    count++;
            }
            return count;
        }

        private static int Test2(List<int> input)
        {
            var count = 0;
            var lastValue = int.MaxValue;
            foreach (var i in Enumerable.Range(2, input.Count-2))
            {
                var currentValue = input[i - 2] + input[i - 1] + input[i];
                if (lastValue < currentValue)
                    count++;
                lastValue = currentValue;
            }
            return count;
        }

    }
}
