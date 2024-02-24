using System.Collections;

namespace ProductsApp
{

    delegate T Map<T>(T x);
    
    static class MyExt
    {
        public static int[] FilterOdd(this int[] nums)
        {
            int[] number = new int[nums.Length/2];
            for (int i = 0; i < nums.Length; i++)
            {
                if((i + 1)%2 == 0)
                    number[i / 2] = nums[i];
            }
        
            return number;
        }
        
        // public static IEnumerable<int> FilterOdd(this int[] nums)
        // {
        //     for (int i = 0; i < nums.Length; i++)
        //     {
        //         if ((i + 1) % 2 == 0)
        //             yield return nums[i];
        //     }
        // }

        public static IEnumerable<T> MapNumbers<T>(this IEnumerable<T> nums, Map<T> map)
        {
            foreach (T num in nums)
            {
                yield return map(num);
            }
        }

        public static string JoinNumbers(this IEnumerable<int> nums)
        {
            return string.Join(", ", nums);
        }
    }
    
    
    internal class Program
    {
        static void Main(string[] args)
        {
            // # Prumer cenu
            // var 1
            IEnumerable<Product> products = GetProducts();
            double? sum = products.Sum(x => x.Price);
            int productCount = products.Count();
            Console.WriteLine(sum / productCount);
            
            // var 2
            double? avg = products.Average(x => x.Price);
            Console.WriteLine(avg);
            
            // # Prumer cena na sklade
            double? avgInStock = products.Where(x => x.Quantity > 0).Average(x => x.Price);
            Console.WriteLine(avgInStock);
            // # Jen nazvy produktu
            string[] product_names = products.Select(x => x.Name).ToArray();

            // # Výběr prvního produktu.
            Product? first = products.FirstOrDefault(x => x.Quantity > 0);
            
            // # Výběr posledniho produktu.
            Product? first = products.FirstOrDefault(x => x.Quantity > 0);
            
            
            // # Rozdeleni produktu
            // TODO
            products.GroupBy(x=> x.Quantity).Select(x=>x.))
            // Calculator calc = new Calculator();
            // calc.OnSetXy += (object obj, MyEventArgs args) => {
            //     Console.WriteLine("Called");
            // };
            // calc.OnCompute += (object obj, MyEventArgs args) =>
            // {
            //     Console.WriteLine(args.Result);
            // };
            //
            // calc.SetXY(2, 10);
            //
            // calc.Execute((x, y) => x + y * 2);


            // int[] nums = new int[] {1, 99, 3, 4, 5, 6, 7, 8, 9};
            // string result = nums.MapNumbers(x => x * x).MapNumbers(x => x/2).JoinNumbers();
            //
            //
            // Console.WriteLine(result);
        }

        static int Sum(int x, int y)
        {
            return x + y;
        }

        static int Multiply(int x, int y)
        {
            return x * y;
        }

        private static IEnumerable<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product(){ Id = 1, Name = "Auto", Price = 700_000, Quantity = 10 },
                new Product(){ Id = 1, Name = "Slon", Price = 1_500_000, Quantity = 0 },
                new Product(){ Id = 1, Name = "Kolo", Price = 26_000, Quantity = 5 },
                new Product(){ Id = 1, Name = "Brusle", Price = 2_800, Quantity = 30 },
                new Product(){ Id = 1, Name = "Hodinky", Price = 18_500, Quantity = 2 },
                new Product(){ Id = 1, Name = "Mobil", Price = 24_000, Quantity = 0 }
            };
        }
    }
}
