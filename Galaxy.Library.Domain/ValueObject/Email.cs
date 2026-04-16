using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.ValueObject
{
    public record Email
    {
        public string Value { get; init; } // Propiedad de solo lectura para el valor del correo electrónico, es init por que asignaremos el valor mediante un metodo

        private Email() {}
        //Patron factory method, para crear una instancia de la clase Email, se utiliza un método estático llamado Create que recibe un valor de correo electrónico y devuelve una nueva instancia de Email. Esto permite encapsular la lógica de validación y creación del objeto en un solo lugar, evitando que se creen instancias inválidas de Email desde fuera de la clase.
        //Hay dos formas de hacerlo, una es mediante un constructor privado y la otra es mediante un método estático que devuelve una nueva instancia de la clase Email. En este caso, se utiliza el constructor privado para validar el valor del correo electrónico y asignarlo a la propiedad Value.
        private Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El correo es obligatorio.");

            if (!value.Contains("@"))
                throw new ArgumentException("El correo no tiene un formato válido.");

            Value = value.Trim().ToLowerInvariant();
        }

        public static Email Crete(string value)
        {
            return new Email(value);
        }

        //public static Email Crete(string value)
        //{
        //    return new Email{
        //    Value = value
        //    };
        //}


    }
} 
