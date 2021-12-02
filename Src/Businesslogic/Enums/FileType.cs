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
        [FileName("input-1.sample.txt")]
        Test1Sample,
        [FileName("input-2.txt")]
        Test2,
        [FileName("input-2.sample.txt")]
        Test2Sample,
    }
}
