using Businesslogic.Attributes;
using Businesslogic.Enums;
using Businesslogic.Extensions;
using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Businesslogic
{
    public static class Helper
    {
        public static List<string> GetFileContents(FileType fileType)
        {
            var filename = fileType.GetAttributeOfType<FileNameAttribute>().FileName;
            var text = System.IO.File.ReadAllText(filename);
            return text.Split(Environment.NewLine).ToList();
        }
        public static void WriteResult(Func<List<string>,int> func, FileType fileType)
        {
            var result1Test = func(GetFileContents(fileType));
            Console.WriteLine($"Result of {fileType} is: {result1Test.ToString().Pastel(Color.Red)}");
        }

        public static void WriteResult(Func<List<string>, long> func, FileType fileType)
        {
            var result1Test = func(GetFileContents(fileType));
            Console.WriteLine($"Result of {fileType} is: {result1Test.ToString().Pastel(Color.Red)}");
        }
    }
}
