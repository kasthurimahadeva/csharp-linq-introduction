using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile<Car>("resource/fuel.csv", ObjectExtensions.ToCar);
            var manufacturers = ProcessFile<Manufacturer>("resource/manufacturers.csv", 
                ObjectExtensions.ToManufacturer);

            var result = from car in cars
                join manufacturer in manufacturers on car.Manufacturer equals manufacturer.Name
                orderby car.Combined descending, car.Name
                select new
                {
                    car.Name,
                    manufacturer.Location,
                    car.Combined
                };

            var result1 = cars.Join(
                    manufacturers,
                    c => c.Manufacturer,
                    m => m.Name,
                    (c, m) => new
                    {
                        c.Name,
                        m.Location,
                        c.Combined
                    })
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name);

            var result2 = from car in cars
                select new {car.Manufacturer, car.Combined}
                into carDetails
                orderby carDetails.Combined descending
                group carDetails by carDetails.Manufacturer
                into carGroup
                orderby carGroup.Key
                select carGroup;

            var result3 = cars
                .Select(c => new {c.Manufacturer, c.Combined})
                .OrderByDescending(c => c.Combined)
                .GroupBy(c => c.Manufacturer)
                .OrderByDescending(c => c.Key);


            var result4 = from manufacturer in manufacturers
                join car in cars on manufacturer.Name equals car.Manufacturer into carGroup
                orderby manufacturer.Name descending
                select new
                {
                    manufacturer.Name,
                    manufacturer.Location,
                    Cars = carGroup
                };

            var result5 = manufacturers
                .GroupJoin(cars,
                    m => m.Name,
                    c => c.Manufacturer,
                    (m, c) => new
                    {
                        m.Name,
                        m.Location,
                        Cars = c.OrderByDescending(car => car.Combined)
                    })
                .OrderByDescending(g => g.Name);
            
            foreach (var group in result4)
            {
                Console.WriteLine($"{group.Name} : {group.Location }has {group.Cars.Count()} cars");
                foreach (var car in group.Cars.Take(10))
                {
                    Console.WriteLine($"{car.Manufacturer, -30} : {car.Combined, 10:N0}");
                }
            }
        }

        private static List<T> ProcessFile<T>(string path, Func<IEnumerable<string>, IEnumerable<T>> function)
        {
            var data = File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1);

            return function(data).ToList();
        }
    }
}