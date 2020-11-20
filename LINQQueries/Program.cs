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
            var cars = ProcessFile("cars.csv");
            foreach (var car in cars)
            {
                Console.WriteLine(car.Name);
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            var query = 
                from line in File.ReadAllLines(path).Skip(1)
                where line.Length > 1
                select Car.FromCsvLine(line);
            return query.ToList();
        }
    }
}