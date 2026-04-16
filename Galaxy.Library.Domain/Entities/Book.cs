using Galaxy.Library.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.Entities
{
    public class Book:BaseEntity
    {
        //public int BookId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        public Isbn Isbn { get; private set; } = default!;
        public int CategoryId { get; private set; }
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }
        //public bool IsActive { get; private set; }

        private Book() { }

        private Book(string title, string author, Isbn isbn, int categoryId, int totalCopies)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("El titulo no puede ser null o vacio", nameof(title));
            if (string.IsNullOrEmpty(Author)) throw new ArgumentNullException("El autor no puede ser null o vacio", nameof(Author));
            if (string.IsNullOrEmpty(isbn.Value)) throw new ArgumentNullException("El codigo indentificador del libro no puede ser null o vacio", nameof(isbn.Value));
            if (categoryId <= 0)
                throw new ArgumentException("La categoría del libro es obligatoria.");

            if (totalCopies <= 0)
                throw new ArgumentException("El total de las copias del libro debe ser mayor a cero.", nameof(totalCopies));

            Title = title;
            Author =author;
            CategoryId = categoryId;
            TotalCopies = totalCopies;
            Isbn = isbn;
            AvailableCopies = totalCopies;
        }

        public static Book Create(string title, string author, Isbn isbn, int categoryId, int totalCopies)
        {

            return new Book
            (
               title,
               author,
               isbn,
               categoryId,
               totalCopies
            );
        }
        public void UpdatBook(string title, string author, Isbn isbn, int categoryId, int totalCopies)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("El titulo no puede ser null o vacio", nameof(title));
            if (string.IsNullOrEmpty(Author)) throw new ArgumentNullException("El autor no puede ser null o vacio", nameof(Author));
            if (string.IsNullOrEmpty(isbn.Value)) throw new ArgumentNullException("El codigo indentificador del libro no puede ser null o vacio", nameof(isbn.Value));
            if (categoryId <= 0)
                throw new ArgumentException("La categoría del libro es obligatoria.");

            Title = title;
            Author = author;
            CategoryId = categoryId;

            if (totalCopies < 0)
                throw new ArgumentException("El total de ejemplares no puede ser negativo.");

            if (totalCopies < (TotalCopies - AvailableCopies))
                throw new ArgumentException("El total de ejemplares no puede ser menor a la cantidad actualmente prestada.");

            var borrowedCopies = TotalCopies - AvailableCopies;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies - borrowedCopies;
            Isbn = isbn;
        }

        public void DecreaseAvailableCopies()
        {
            if (!IsActive)
                throw new ArgumentException("No se puede prestar un libro inactivo.");

            if (AvailableCopies <= 0)
                throw new ArgumentException("No hay ejemplares disponibles para préstamo.");

            AvailableCopies--;
        }

        public void IncreaseAvailableCopies()
        {
            if (AvailableCopies >= TotalCopies)
                throw new ArgumentException("Los ejemplares disponibles no pueden superar el total.");

            AvailableCopies++;
        }

        public void Disable()
        {
            Delete();
        }

    }
}
