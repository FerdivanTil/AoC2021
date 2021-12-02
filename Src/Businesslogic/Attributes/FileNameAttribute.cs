using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslogic.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class FileNameAttribute: Attribute
    {
        public string FileName { get; set; }
        public FileNameAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}
