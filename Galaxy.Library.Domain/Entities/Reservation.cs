using Galaxy.Library.Domain.Enum;
using Galaxy.Library.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        //public int ReservationId { get; private set; }
        public Guid BookId { get; private set; }
        public Book Book { get; private set; } = default!;
        public Guid ReaderId { get; private set; }
        public Reader Reader { get; private set; } = default!;
        public DateTime ReservationDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public ReservationStatus Status { get; private set; } = default!;
        //public bool IsActive { get; private set; }

        private Reservation() { }

        private Reservation(Book book, Reader reader, ReservationPeriod period) {
            if (book == null)
                throw new ArgumentException("El libro es obligatorio para registrar una reserva.");

            if (reader == null)
                throw new ArgumentException("El lector es obligatorio para registrar una reserva.");

            Book = book;
            BookId = book.Id;
            Reader = reader;
            ReaderId = reader.Id;
            ReservationDate = period.ReservationDate;
            ExpirationDate = period.ExpirationDate;
            Status = ReservationStatus.Pending;
        }

        public static Reservation Create(Book book, Reader reader, ReservationPeriod period)
        {
            return new Reservation
            (
               book,
               reader,
               period
            );
        }

        public void Update(ReservationPeriod period)
        {
            if (Status != ReservationStatus.Pending)
                throw new ArgumentException("Solo una reserva pendiente puede ser actualizada.");
            ReservationDate = period.ReservationDate;
            ExpirationDate = period.ExpirationDate;
        }

        public void Fulfill()
        {
            if (Status != ReservationStatus.Pending)
                throw new ArgumentException("Solo una reserva pendiente puede ser atendida.");

            if (DateTime.UtcNow > ExpirationDate)
                throw new ArgumentException("La reserva ya expiró y no puede ser atendida.");


            Status = ReservationStatus.Fulfilled;
        }

        public void Expire()
        {
            if (Status != ReservationStatus.Pending)
                throw new ArgumentException("Solo una reserva pendiente puede expirar.");

            Status = ReservationStatus.Expired;
        }

        public void Cancel()
        {
            if (Status == ReservationStatus.Fulfilled   )
                throw new ArgumentException("No se puede cancelar una reserva ya atendida.");

            Status = ReservationStatus.Cancelled;
        }

        public void Disable()
        {
            Delete();
        }


    }
}
