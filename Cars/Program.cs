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
                .Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name);

            foreach (var car in query.Take(10))
            {
                car.Log();
            }
            
            Console.WriteLine("*******************");
            
            foreach (var car in query2.Take(10))
            {
                car.Log();
            }

            // foreach (var car in cars.Take(10))
            // {
            //     car.Log();
            // }
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