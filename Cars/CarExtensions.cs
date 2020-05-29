using System;

namespace Cars
{
    public static class CarExtensions
    {
        public static void Log(this Car car)
        {
            Console.WriteLine($"{car.Name, -30} : {car.Combined, -10:N0}");
        }
    }
}