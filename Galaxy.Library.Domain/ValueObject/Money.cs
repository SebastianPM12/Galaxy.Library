using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.ValueObject
{
    public record class Money
    {
        public decimal Value { get; }

        private Money() {}

        private Money(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("El monto no puede ser negativo.");

            Value = decimal.Round(value, 2);
        }

        public static Money Create(decimal value)
        {
            return new Money(value);
        }

    }
}
