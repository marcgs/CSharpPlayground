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
            Console.WriteLine("----- CARS ------");
            var joinQuery = from car in cars
                join manufacturer in manufacturers on (car.Manufacturer, car.Year) equals (manufacturer.Name, manufacturer.Year) 
                orderby car.Combined descending, car.Name
                select new
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
                };

            foreach (var car in joinQuery.Take(10))
            {
                Console.WriteLine($"{car.Name} ({car.Headquarters}) : {car.Combined}");
            }
            
            // group by
            Console.WriteLine("----- MANUFACTURERS ------");
            var groupByQuery = from car in cars
                join manufacturer in manufacturers on (car.Manufacturer, car.Year) equals (manufacturer.Name, manufacturer.Year)
                group car by (manufacturer.Name, manufacturer.Year);
            
            foreach (var group in groupByQuery)
            {
                Console.WriteLine($"{group.Key.Name} has {group.Count()} cars in {group.Key.Year}. Top 2 efficient:");
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"  {car.Name} : {car.Combined}");
                }
            }
            
            // joinGroup
            Console.WriteLine("----- COUNTRIES ------");
            var joinGroupQuery = from manufacturer in manufacturers
                join car in cars on (manufacturer.Name, manufacturer.Year) equals (car.Manufacturer, car.Year) 
                    into carGroup
                select new
                {
                     Manufacturer = manufacturer,
                     Cars = carGroup
                } into result
                group result by result.Manufacturer.Headquarters;

            foreach (var result in joinGroupQuery)
            {
                Console.WriteLine($"{result.Key} has {result.Count()} cars. Top 2 efficient:");
                foreach (var car in result.SelectMany(g => g.Cars).OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"  {car.Name} : {car.Combined}");
                }
            }
            
            Console.WriteLine("----- STATISTICS ------");
            var groupStats = from car in cars
                group car by car.Manufacturer
                into carGroup
                select new
                {
                    Name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined),
                } into result
                orderby result.Max descending 
                select result;
            
            foreach (var groupStat in groupStats)
            {
                Console.WriteLine($"{groupStat.Name}");
                Console.WriteLine($"  Max: {groupStat.Max}");
                Console.WriteLine($"  Min: {groupStat.Min}");
                Console.WriteLine($"  Avg: {groupStat.Avg}");
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