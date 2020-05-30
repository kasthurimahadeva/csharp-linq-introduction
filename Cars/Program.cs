using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("resource/fuel.csv");
        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToCar()
                .ToList();
        }
    }
}