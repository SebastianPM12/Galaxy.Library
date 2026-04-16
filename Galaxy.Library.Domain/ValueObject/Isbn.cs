using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.ValueObject
{
    public record class Isbn
    {
        public string Value { get; }

        private Isbn() { }


        private Isbn(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El ISBN es obligatorio.");

            var clean = value.Replace("-", "").Trim();

            if (clean.Length != 10 && clean.Length != 13)
                throw new ArgumentException("El ISBN debe tener 10 o 13 caracteres.");

            Value = clean;
        }

        public static Isbn Crete(string value)
        {
            return new Isbn(value);
        }
    }
}
