using System;

namespace LINQQueries
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars("cars.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = from car in cars
                join manufacturer in manufacturers on (car.Manufacturer, car.Year) equals (manufacturer.Name, manufacturer.Year) 
                orderby car.Combined descending, car.Name
                select new
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
                };

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcessCars(string path)
        {
            var query = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .ToCar();
            
            return query.ToList();
        }
        
        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .ToManufacturer();
            
            return query.ToList();
        }
    }
}