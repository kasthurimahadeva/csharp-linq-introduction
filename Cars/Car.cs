namespace Cars
{
    public class Car
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public double Displacement { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }
        
        public static Car TransformToCar(string line)
        {
            var str = line.Split(",");
            return new Car
            {
                Year = int.Parse(str[0]),
                Manufacturer = str[1],
                Name = str[2],
                Displacement = double.Parse(str[4]),
                Cylinders = int.Parse(str[5]),
                City = int.Parse(str[6]),
                Highway = int.Parse(str[6]),
                Combined = int.Parse(str[7])
            };
        }
    }
}