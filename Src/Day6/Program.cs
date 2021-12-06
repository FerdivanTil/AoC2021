using Businesslogic;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Pastel;
using System.Drawing;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper.WriteResult(Test1, FileType.Test1Sample);
            Helper.WriteResult(Test1, FileType.Test1);
            Helper.WriteResult(Test2, FileType.Test1Sample);
            Helper.WriteResult(Test2, FileType.Test1);
        }

        private static long Test1(List<string> input)
        {
            var fish = input[0].Split(",").Select(i => new Fish(Convert.ToInt32(i))).ToList();
            foreach (var i in Enumerable.Range(1, 80))
            {
                fish.ForEach(i => i.NewDay());
            }
            return fish.Sum(i => i.Amount);
        }

        private static long Test2(List<string> input)
        {
            var fishs = input[0].Split(",").Select(i => Convert.ToInt32(i)).ToList();
            var list = new List<long>();
            const int days = 256;

            // Create an empty list and make sure we have more then we need (+10)
            foreach (var i in Enumerable.Range(1, days +10))
            {
                list.Add(0);
            }
            // Initialise the fish
            foreach(var fish in fishs)
            {
                list[fish + 1]++;
            }
            
            // Run every day and set the spawn time
            foreach (var day in Enumerable.Range(1, days))
            {
                var spawn = list[day];//.Spawn;
                list[day + 9] += spawn;
                list[day + 7] += spawn;
            }
            // Take days +1 because we start at day 0 and add the initial amount of fish
            return list.Take(days+1).Sum(i => i)+ fishs.Count();
        }
    }

    [DebuggerDisplay("Days = {State} and Childern = {Children.Count}")]
    public class Fish
    {
        public int State { get; set; } = 8;
        public List<Fish> Children { get; set; } = new List<Fish>();
        public long Amount => Children.Sum(i => i.Amount) + 1;
        public Fish()
        {

        }
        public Fish(int state)
        {
            State = state;
        }
        public void NewDay()
        {
            Children.ForEach(i => i.NewDay());
            if(State == 0)
            {
                State = 6;
                Children.Add(new Fish());
                return;
            }
            State--;
        }
        
    }
}
