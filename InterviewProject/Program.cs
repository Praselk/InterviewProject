using InterviewProject.Task1;
using InterviewProject.Task3;
using InterviewProject.Task4;

namespace InterviewProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task 1
            (new Thread(() =>
            {
                LazyBasedSingleton singleton1 = LazyBasedSingleton.Instance;
            })).Start();

            LazyBasedSingleton singleton2 = LazyBasedSingleton.Instance;

            //Task 2
            Dictionary<Task2.Price, string> dict = new Dictionary<Task2.Price, string>();
            try
            {
                dict.Add(new Task2.Price("руб.", 1000), "Тест1");
                dict.Add(new Task2.Price("руб.", 2000), "Тест2");
                dict.Add(new Task2.Price("руб.", 1000), "Тест3"); //Should lead to an argument exception
            } 
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Task 3
            IEnumerable<int> items = new List<int>() { 1, 2, 3, 4 };
            IEnumerable<int> result = items.MyWhere(i => i % 2 == 0);
            foreach (int i in result)
                Console.WriteLine(i);

            //Task 4
            Random random = new Random();
            string[] currencies = new[] { "RUB", "USD" };
            var products = Enumerable.Range(0, 1000).Select(x => new Product() { Id = x, Name = $"Product{x}" }).ToList();
            var prices = Enumerable.Range(0, 100000)
                .Select(x => new Price() { Amount = random.Next(1000), Currency = currencies[random.Next(2)], ProductId = random.Next(1000) })
                .ToList();

            var test = new Test();
            var task4ResultOriginal = test.ComposeProductRubPrices(products, prices);
            var task4ResultOptimized = test.ComposeProductRubPricesOptimized(products, prices);

            //Task 5
            ExecuteTask5().Wait();
        }

        static async Task ExecuteTask5()
        {
            var numbers = Enumerable.Range(0, 100);
            var numbersAsyncEnum = GetNumbersAsync(numbers);
            var task5Result = numbersAsyncEnum.ToBatchesAsync(10);

            await foreach (var batch in task5Result)
                Console.WriteLine(string.Join(", ", batch));
        }

        static async IAsyncEnumerable<int> GetNumbersAsync(IEnumerable<int> integers)
        {
            foreach (var n in integers)
            {
                await Task.Delay(100);
                yield return n;
            }
        }
    }
}
