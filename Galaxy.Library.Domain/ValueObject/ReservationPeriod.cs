using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.ValueObject
{
    public record class ReservationPeriod
    {
        public DateTime ReservationDate { get; }
        public DateTime ExpirationDate { get; }

        private ReservationPeriod() { }
        private ReservationPeriod(DateTime reservationDate, DateTime expirationDate)
        {
            if (expirationDate <= reservationDate)
                throw new ArgumentException("La fecha de expiración debe ser posterior a la fecha de reserva.");

            ReservationDate = reservationDate;
            ExpirationDate = expirationDate;
        }


        public static ReservationPeriod Create(DateTime reservationDate, DateTime expirationDate)
        {
            return new ReservationPeriod(reservationDate, expirationDate);
        }
    }
}
