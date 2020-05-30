using System;
using System.Collections.Generic;
using System.Linq;

namespace Cars
{
    public static class CarExtensions
    {
        public static void Log(this Car car)
        {
            Console.WriteLine($"{car.Name, -30} : {car.Combined, -10:N0}");
        }

        public static IEnumerable<Car> ToCar(this IEnumerable<string> lines)
        {
            return lines.Select(l => l.Split(","))
                .Select(str => new Car
                {
                    Year = int.Parse(str[0]),
                    Manufacturer = str[1],
                    Name = str[2],
                    Displacement = double.Parse(str[4]),
                    Cylinders = int.Parse(str[5]),
                    City = int.Parse(str[6]),
                    Highway = int.Parse(str[6]),
                    Combined = int.Parse(str[7])
                });
        }
    }
}