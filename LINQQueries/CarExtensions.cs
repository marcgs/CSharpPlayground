namespace LINQQueries
{
    using System.Collections.Generic;

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var cols = line.Split(",");
                yield return new Car
                {
                    Year = int.Parse(cols[0]),
                    Manufacturer = cols[1],
                    Name = cols[2],
                    Displacement = double.Parse(cols[3]),
                    Cylinders = int.Parse(cols[4]),
                    City = int.Parse(cols[5]),
                    Highway = int.Parse(cols[6]),
                    Combined = int.Parse(cols[7])
                };
            }
        }
    }
}