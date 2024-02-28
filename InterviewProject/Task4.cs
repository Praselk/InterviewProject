using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProject.Task4
{
    public class Test
    {
        public IList<ProductRubPrice> ComposeProductRubPrices(IList<Product> products, IList<Price> prices)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var productPrices = new List<ProductRubPrice>();

            foreach (var product in products)
            {
                var filteredPrices = prices.Where(p => p.ProductId == product.Id && p.Currency == "RUB")
                    .Select(p => new ProductRubPrice { Amount = p.Amount, ProductName = product.Name })
                    .ToList();

                if (filteredPrices.Any())
                {
                    productPrices.AddRange(filteredPrices);
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Original: " + stopwatch.ElapsedMilliseconds);

            return productPrices.Distinct().ToList();
        }

        public IList<ProductRubPrice> ComposeProductRubPricesOptimized(IList<Product> products, IList<Price> prices)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var productDict = products.ToDictionary(x => x.Id, x => x.Name);
            var result = prices
                .Where(x => productDict.Keys.Contains(x.ProductId) && x.Currency == "RUB")
                .ToLookup(x => productDict[x.ProductId])
                .SelectMany(x => x.Select(p => new ProductRubPrice { Amount = p.Amount, ProductName = x.Key }))
                .Distinct()
                .ToList();
            stopwatch.Stop();
            Console.WriteLine("Optimized: " + stopwatch.ElapsedMilliseconds);

            return result;
        }
    }

    public class Product
    {
        public int Id;
        public string Name;
    }

    public class Price
    {
        public int ProductId;
        public decimal Amount;
        public string Currency;
    }

    public class ProductRubPrice
    {
        public string ProductName;
        public decimal Amount;
    }

}
