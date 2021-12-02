using Businesslogic.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslogic.Enums
{
    public enum FileType
    {
        [FileName("input-1.txt")]
        Test1,
        [FileName("input-1-test.txt")]
        Test1Test,
        [FileName("input-2.txt")]
        Test2
    }
}
