using Galaxy.Library.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.Entities
{
    public class Category :BaseEntity
    {
        //public int CategoryId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        //public bool IsActive { get; private set; }
        private readonly List<Book> _books = new(); //la privada es para navegar las relaciones que tenga con EF 
        public IReadOnlyCollection<Book> Books => _books.AsReadOnly();
        private Category() { }


        private Category(string name, string? description)
        {
            if (name.Trim().Length > 100)
                throw new ArgumentException("El nombre de la categoría no debe superar los 100 caracteres.");

            if (description.Length > 250)
                throw new ArgumentException("La descripción de la categoría no debe superar los 250 caracteres.");

            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("El nombre no puede ser null o vacio", nameof(name));

            Name = name;
            Description = description?.Trim();
        }

        public static Category Create(string name, string? description)
        {

            return new Category
            (
               name,
               description
            );
        }

        public void UpdateDeCategory(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("El nombre de la categoría es obligatorio.");

            if (name.Trim().Length > 100)
                throw new ArgumentException("El nombre de la categoría no debe superar los 100 caracteres.");

            Name=name;
            Description = description?.Trim();
        }



        public void Disable()
        {
            Delete();
        }


    }
}
