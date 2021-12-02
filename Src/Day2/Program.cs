using Businesslogic;
using Businesslogic.Enums;
using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day2
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

        private static int Test1(List<string> input)
        {
            var horizontal = 0;
            var depth = 0;
            var courses = Course.Parse(input);

            horizontal = courses.Where(i => i.Direction == Direction.Forward).Sum(i => i.Amount);
            depth += courses.Where(i => i.Direction == Direction.Down).Sum(i => i.Amount);
            depth -= courses.Where(i => i.Direction == Direction.Up).Sum(i => i.Amount);

            return horizontal * depth;
        }
        private static int Test2(List<string> input)
        {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var course in Course.Parse(input))
            {
                switch(course.Direction)
                {
                    case Direction.Down:
                        aim += course.Amount;
                        break;
                    case Direction.Up:
                        aim -= course.Amount;
                        break;
                    case Direction.Forward:
                        horizontal += course.Amount;
                        depth  += course.Amount * aim;
                        break;
                }
            }

            return horizontal * depth;
        }

        private class Course
        {
            public Direction Direction { get; set; }
            public int Amount { get; set; }
            public static List<Course> Parse(List<string> input)
            {
                return input.Select(i => new Course { Direction = Enum.Parse<Direction>(i.Split(" ")[0], true) , Amount = Convert.ToInt32(i.Split(" ")[1]) }).ToList();
            }
        }
        private enum Direction
        {
            Forward,
            Down,
            Up
        }
    }
}
