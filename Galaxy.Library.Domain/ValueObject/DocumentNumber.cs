using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.ValueObject
{
    public record DocumentNumber
    {
        public string Value { get; init; }
        public string Type { get; init; }

        private DocumentNumber() { }


        public DocumentNumber(string value, string type)
        {

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("El tipo de documento es obligatorio.");

            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("El tipo de documento no debe ser null o vacio.");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El número de documento es obligatorio.");

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("El número de documento no debe ser null o vacio.");

            Value = value.Trim();
            Type = type.Trim();
        }

        public static DocumentNumber Crete(string value, string type)
        {
            return new DocumentNumber(value,type);
        }
    }
}
