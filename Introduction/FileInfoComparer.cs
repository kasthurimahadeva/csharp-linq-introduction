using System;
using System.Collections.Generic;
using System.IO;

namespace Introduction
{
    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo file1, FileInfo file2)
        {
            if (file1 != null && file2 != null)
            {
                return file2.Length.CompareTo(file1.Length);
            }
            return 0;
        }
    }
}