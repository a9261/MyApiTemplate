using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Money
    {
        public string Currency { get; }
        public decimal Amount { get; }

        public Money(decimal amount, string currency = "")
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public bool IsPositiveOrZero()
        {
            return this.Amount >= 0m;
        }

        public bool IsNegative()
        {
            return this.Amount < 0m;
        }
    }
}