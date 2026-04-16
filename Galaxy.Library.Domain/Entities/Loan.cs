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
        public int BookId { get; private set; }
        public int ReaderId { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public LoanStatus Status { get; private set; } = default!;
        //public bool IsActive { get; private set; }

        protected Loan() { }

        private Loan(int bookId, int readerId, LoanPeriod period)
        {
            if (bookId <= 0)
                throw new ArgumentException("El libro es obligatorio para registrar un préstamo.");

            if (readerId <= 0)
                throw new ArgumentException("El lector es obligatorio para registrar un préstamo.");

            BookId = bookId;
            ReaderId = readerId;
            LoanDate = period.LoanDate;
            DueDate = period.DueDate;
            Status = LoanStatus.Active;
        }

        public static Loan Create(int bookId, int readerId, LoanPeriod period)
        {

            return new Loan
            (
               bookId,
               readerId,
               period
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
