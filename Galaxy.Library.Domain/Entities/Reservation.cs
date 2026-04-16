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
        public int BookId { get; private set; }
        public int ReaderId { get; private set; }
        public DateTime ReservationDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public ReservationStatus Status { get; private set; } = default!;
        //public bool IsActive { get; private set; }

        private Reservation() { }

        private Reservation(int bookId,int readerId, ReservationPeriod period) {
            if (bookId <= 0)
                throw new ArgumentException("El libro es obligatorio para registrar una reserva.");

            if (readerId <= 0)
                throw new ArgumentException("El lector es obligatorio para registrar una reserva.");

            BookId = bookId;
            ReaderId = readerId;
            ReservationDate = period.ReservationDate;
            ExpirationDate = period.ExpirationDate;
            Status = ReservationStatus.Pending;
        }

        public static Reservation Create(int bookId, int readerId, ReservationPeriod period)
        {
            return new Reservation
            (
               bookId,
               readerId,
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
