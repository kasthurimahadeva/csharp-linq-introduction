﻿using System;
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