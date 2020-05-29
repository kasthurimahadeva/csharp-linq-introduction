using System;
using System.IO;
using System.Linq;

namespace Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Windows";
            FileCompareWithoutLinq(path);
            Console.WriteLine("*************");
            FileCompareWithLinq(path);
        }

        private static void FileCompareWithLinq(string path)
        {
            // var query1 = from file in new DirectoryInfo(path).GetFiles() 
            //     orderby file.Length descending select file;
            //
            // foreach (var fileInfo in query1.Take(5))
            // {
            //     Console.WriteLine($"{fileInfo.Name, -25} : {fileInfo.Length, 15:N0}");
            // }
            
            var query = Directory.GetFiles(path).Select(f => new FileInfo(f)).
                OrderByDescending(f => f.Length).Take(5);
            foreach (var fileInfo in query)
            {
                Console.WriteLine($"{fileInfo.Name, -25} : {fileInfo.Length, 15:N0}");
            }
        }

        private static void FileCompareWithoutLinq(string path)
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            // Array.Sort(files, (x, y) => 
            //     (int) (y.Length - x.Length));
            Array.Sort(files, new FileInfoComparer());
            
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine($"{files[i].Name, -25} : {files[i].Length, 15:N0}");
            }
        }
    }
}