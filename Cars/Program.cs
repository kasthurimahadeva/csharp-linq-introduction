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

            var query = from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016
                orderby car.Combined descending, car.Name
                select car;

            var query2 = cars
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name)
                .First(c => c.Manufacturer == "BMW" && c.Year == 2016);
            
            var query3 = cars
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name)
                .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016);
            
            query2.Log();
            query3?.Log();
        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .Select(Car.TransformToCar)
                .ToList();
        }
    }
}