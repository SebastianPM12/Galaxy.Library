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
        public Category Category { get; private set; } = default!;
        public string Description { get; private set; } = string.Empty;
        public Guid CategoryId { get; private set; }
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }
        //public bool IsActive { get; private set; }

        private readonly List<Loan> _loans = new(); 
        public IReadOnlyCollection<Loan> Loans => _loans.AsReadOnly();

        private readonly List<Reservation> _reservations = new();
        public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();

        private Book() { }

        private Book(string title, string author, Isbn isbn, Category category, int totalCopies, string description)
        {

            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("El titulo no puede ser null o vacio", nameof(title));
            if (string.IsNullOrEmpty(author)) throw new ArgumentNullException("El autor no puede ser null o vacio", nameof(author));
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException("La descripción no puede ser null o vacio", nameof(description));

            if (string.IsNullOrEmpty(isbn.Value)) throw new ArgumentNullException("El codigo indentificador del libro no puede ser null o vacio", nameof(isbn.Value));
            if (category == null)
                throw new ArgumentException("La categoría del libro es obligatoria.");

            if (totalCopies <= 0)
                throw new ArgumentException("El total de las copias del libro debe ser mayor a cero.", nameof(totalCopies));

            Title = title;
            Author =author;
            Category = category;
            CategoryId = category.Id;
            TotalCopies = totalCopies;
            Isbn = isbn;
            AvailableCopies = totalCopies;
        }

        public static Book Create(string title, string author, Isbn isbn, Category category, int totalCopies, string description)
        {

            return new Book
            (
               title,
               author,
               isbn,
               category,
               totalCopies,
               description
            );
        }
        public void UpdatBook(string title, string author, Isbn isbn, Guid categoryId, int totalCopies)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("El titulo no puede ser null o vacio", nameof(title));
            if (string.IsNullOrEmpty(Author)) throw new ArgumentNullException("El autor no puede ser null o vacio", nameof(Author));
            if (string.IsNullOrEmpty(isbn.Value)) throw new ArgumentNullException("El codigo indentificador del libro no puede ser null o vacio", nameof(isbn.Value));
            if (categoryId == Guid.Empty)
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
