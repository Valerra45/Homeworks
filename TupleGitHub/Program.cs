using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задание_1
{
    class Car
    {
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        public Car(string Mark, string Model, string Color)
        {
            this.Mark = Mark;
            this.Model = Model;
            this.Color = Color;
        }

        public Car(string Mark, string Model, string Color, decimal Price) : this(Mark, Model, Color)
        {
            this.Price = Price;
        }

        public override string ToString()
        {
            return $"{Mark} {Model} цвет: {Color} цена: {Price}";
        }
    }

    class Program
    {
        /// <summary>
        /// Возвращает список автомобилей с ценой больше 
        /// заданной.
        /// </summary>
        /// <param name="Price"></param>
        /// <param name="Cars"></param>
        /// <returns>
        /// Список автомобилкй
        /// </returns>
        public static List<Car> PriceMore(decimal Price, List<Car> Cars) => Cars.Where(p => p.Price > Price).ToList<Car>();

        /// <summary>
        /// Возвращает список автомобилей заданного цветв
        /// </summary>
        /// <param name="Color"></param>
        /// <param name="Cars"></param>
        /// <returns>
        /// Список автомобилей
        /// </returns>
        public static List<Car> SelectByColor(string Color, List<Car> Cars) => Cars.Where(p => p.Color == Color).ToList<Car>();

        /// <summary>
        /// Возвращает список автомобилей заданной марки с заданной ценой
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="Cars"></param>
        /// <returns>
        /// Список автомобилей
        /// </returns>
        public static List<Car> SelectByPriceEndMark((decimal Price, string Mark) tuple, List<Car> Cars)
            => Cars.Where(p => p.Price == tuple.Price && p.Mark == tuple.Mark).ToList();

        /// <summary>
        /// Возвращает сумму стоимости всех автомобилей
        /// </summary>
        /// <param name="Cars"></param>
        /// <returns>
        /// Сумма стоимости автомобилей
        /// </returns>
        public static decimal SumPrice(List<Car> Cars) => Cars.Sum(p => p.Price);

        /// <summary>
        /// Возвращаяет количество автомобилей заданного цвета
        /// </summary>
        /// <param name="Color"></param>
        /// <param name="Cars"></param> 
        /// <returns>
        /// Количество автомобилей заданного цвета
        /// </returns>
        public static int CountColors(string Color, List<Car> Cars) => Cars.Count(p => p.Color == Color);

        /// <summary>
        /// Возвращает марку и модель автомобиля с минимальной ценой
        /// </summary>
        /// <param name="Price"></param>
        /// <param name="Cars"></param>
        /// <returns>
        /// Список кортежей марка модель 
        /// </returns>
        public static List<(string, string)> SelectByMinPrice(decimal Price, List<Car> Cars) => Cars
                   .Where(p => p.Price < 5_000m)
                   .Select(p => (p.Mark, p.Model)).ToList();

        /// <summary>
        /// Возврашает кортеж - список авто по диапазону цены и
        /// количество авто двух цветов
        /// </summary>
        /// <param name="TuplePrice"></param>
        /// <param name="TupleColor"></param>
        /// <param name="Cars"></param>
        /// <returns>
        /// Возврашает кортеж- список авто по диапазону цены и количество авто двух цветов
        /// </returns>
        public static (List<Car> c, int count1, int count2) SelectEndCount((decimal MinPrice, decimal MaxPrice) TuplePrice, (string Color1, string Color2) TupleColor, List<Car> Cars) => (
                    Cars.Where(p => p.Price <= TuplePrice.MaxPrice && p.Price >= TuplePrice.MinPrice).ToList(),
                    Cars.Count(p => p.Color == TupleColor.Color1),
                    Cars.Count(p => p.Color == TupleColor.Color2)
                    );

        static void Main(string[] args)
        {
            List<Car> Cars = new List<Car>
            {
                new Car("Kia", "Rio",  "Красный", 10_100m),
                new Car("Kia", "Picanto",  "Красный", 9_000m),
                new Car("Audi", "A3",  "Синий", 19_000m),
                new Car("BMW", "X1",  "Белый", 24_000m),
                new Car("Lada", "Granta",  "Серый", 4_700m),
                new Car("Lada", "Granta",  "Белый", 4_500m)
            };

            Cars.Add(new Car("BMW", "X3", "Черный", 34_000m));

            var CarsList = PriceMore(10_000m, Cars);

            Console.WriteLine("Автомобили с ценой больше 10_000:");
            foreach (var car in CarsList)
            {
                Console.WriteLine(car);
            }

            Console.WriteLine();
            CarsList = SelectByColor("Красный", Cars);
            Console.WriteLine("Автомобили красного цвета:");
            foreach (var car in CarsList)
            {
                Console.WriteLine(car);
            }

            Console.WriteLine();
            CarsList = SelectByPriceEndMark((34_000m, "BMW"), Cars);
            Console.WriteLine("Автомобили BMW цена 34_000:");
            foreach (var car in CarsList)
            {
                Console.WriteLine(car);
            }

            Console.WriteLine();
            Console.WriteLine($"Сумма стоимости всех автомобилей: {SumPrice(Cars)}");

            Console.WriteLine();
            Console.WriteLine($"Количество  автомобилей красного цвета: {CountColors("Красный", Cars)}");

            Console.WriteLine();
            Console.WriteLine("Марки и модели дешевле 5000:");
            var ModelList = SelectByMinPrice(5000m, Cars);
            foreach (var m in ModelList)
            {
                Console.WriteLine($"{m.Item1} {m.Item2}");
            }

            Console.WriteLine();
            Console.WriteLine("Автомобили с ценой 10_000 - 20_000 и количество авто красного и черного цветов:");
            var rez = SelectEndCount((10_000m, 20_000), ("Красный", "Черный"), Cars);
            foreach (var car in rez.c)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine($"{rez.count1}, {rez.count2}");
        }
    }
}
