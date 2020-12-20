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

            // join query
            var joinQuery = from car in cars
                join manufacturer in manufacturers on (car.Manufacturer, car.Year) equals (manufacturer.Name, manufacturer.Year) 
                orderby car.Combined descending, car.Name
                select new
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
                };

            Console.WriteLine("----- CARS ------");
            foreach (var car in joinQuery.Take(10))
            {
                Console.WriteLine($"{car.Name} ({car.Headquarters}) : {car.Combined}");
            }
            
            // group by
            var groupByQuery = from car in cars
                join manufacturer in manufacturers on (car.Manufacturer, car.Year) equals (manufacturer.Name, manufacturer.Year)
                group car by (manufacturer.Name, manufacturer.Year);
            
            Console.WriteLine("----- MANUFACTURERS ------");
            foreach (var group in groupByQuery)
            {
                Console.WriteLine($"{group.Key.Name} has {group.Count()} cars in {group.Key.Year}. Top 2 efficient:");
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"  {car.Name} : {car.Combined}");
                }
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