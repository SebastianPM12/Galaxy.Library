using Galaxy.Library.Domain.Enum;
using Galaxy.Library.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.Entities
{
    public class Loan:BaseEntity
    {
        //public int LoanId { get; private set; }
        public Guid BookId { get; private set; }
        public Book Book { get; private set; } = default!;
        public Guid ReaderId { get; private set; }
        public Reader Reader { get; private set; } = default!;
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public LoanStatus Status { get; private set; } = default!;
        //public bool IsActive { get; private set; }
        public Penalty? Penalty { get; private set; }
        protected Loan() { }

        private Loan( Reader reader, Book book, LoanPeriod period, Penalty? penalty = null)
        {

            if (book == null)
                throw new ArgumentException("El libro es obligatorio para registrar un préstamo.");

            if (reader == null)
                throw new ArgumentException("El lector es obligatorio para registrar un préstamo.");

            Penalty = penalty;
            Book = book;
            BookId = book.Id;
            Reader = reader;
            ReaderId = reader.Id;
            LoanDate = period.LoanDate;
            DueDate = period.DueDate;
            Status = LoanStatus.Active;
        }

        public static Loan Create( Reader reader, Book book, LoanPeriod period, Penalty? penalty = null)
        {

            return new Loan
            (
               reader,
               book,
               period,
               penalty
            );
        }

        public void UpdateLoanPeriod(LoanPeriod period)
        {
            if (Status == LoanStatus.Returned)
                throw new ArgumentException("No se puede actualizar el período de un préstamo devuelto.");
            if (Status == LoanStatus.Cancelled)
                throw new ArgumentException("No se puede actualizar el período de un préstamo cancelado.");
            LoanDate = period.LoanDate;
            DueDate = period.DueDate;
        }


        public void MarkAsReturned(DateTime returnDate)
        {
            if (Status == LoanStatus.Returned)
                throw new ArgumentException("El préstamo ya fue devuelto.");

            if (Status == LoanStatus.Cancelled)
                throw new ArgumentException("No se puede devolver un préstamo cancelado.");

            if (returnDate < LoanDate)
                throw new ArgumentException("La fecha de devolución no puede ser menor a la fecha de préstamo.", nameof(returnDate));

            ReturnDate = returnDate;
            Status = LoanStatus.Returned;
        }

        public void MarkAsOverdue()
        {
            if (Status == LoanStatus.Returned || Status == LoanStatus.Cancelled)
                throw new ArgumentException("No se puede marcar como vencido un préstamo devuelto o cancelado.");

            Status = LoanStatus.Overdue;
        }

        public void Cancel()
        {
            if (Status == LoanStatus.Returned)
                throw new ArgumentException("No se puede cancelar un préstamo ya devuelto.");

            Status = LoanStatus.Cancelled;
        }


        public void Disable()
        {
            Delete();
        }

    }
}
