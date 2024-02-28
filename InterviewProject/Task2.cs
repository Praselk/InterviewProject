using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProject.Task2
{
    public class Price
    {
        public Price(string currency, double amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public override bool Equals(object? obj)
        {
            Price otherPrice = obj as Price;
            if (otherPrice == null) 
                return false;

            return this.Amount == otherPrice.Amount && this.Currency == otherPrice.Currency;
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = (hash * 23) + (Currency ?? string.Empty).GetHashCode();
            hash = (hash * 23) + Amount.GetHashCode();
            return hash;
        }

        public string Currency { get; set; }
        public double Amount { get; set; }
    }
}
