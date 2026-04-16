using Galaxy.Library.Domain.Enum;
using Galaxy.Library.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.Entities
{
    public class Penalty
    {
        //public int PenaltyId { get; private set; }
        public int LoanId { get; private set; }
        public Money Amount { get; private set; } = default!;
        public string Reason { get; private set; } = string.Empty;
        public PenaltyStatus Status { get; private set; } = default!;
        //public bool IsActive { get; private set; }

        private Penalty() { }

        private Penalty(int loanId, Money amount, string reason)
        {
            if (loanId <= 0)
                throw new ArgumentException("El préstamo es obligatorio para registrar una multa.");
            if (amount.Value <= 0)
                throw new ArgumentException("El monto de la multa debe ser mayor a cero.", nameof(amount));
            if (string.IsNullOrEmpty(reason))
                throw new ArgumentException("La razón de la multa es obligatoria.", nameof(reason));
            LoanId = loanId;
            Amount = amount;
            Reason = reason;
            Status = PenaltyStatus.Pending;
        }

        public static Penalty Create(int loanId, Money amount, string reason)
        {
            return new Penalty
            (
               loanId,
               amount,
               reason
            );
        }

        public void UpdatePenalty(Money amount, string reason)
        {
            if (Status == PenaltyStatus.Paid)
                throw new ArgumentException("No se puede modificar el monto de una penalidad ya pagada.");
            if (Status == PenaltyStatus.Cancelled)
                throw new ArgumentException("No se puede modificar el monto de una penalidad cancelada.");
            if (amount.Value <= 0)
                throw new ArgumentException("El monto de la multa debe ser mayor a cero.", nameof(amount));

            if (string.IsNullOrEmpty(reason))
                throw new ArgumentException("La razón de la multa es obligatoria.", nameof(reason));

            Amount = amount;
            Reason= reason;
        }

        public void MarkAsPaid()
        {
            if (Status == PenaltyStatus.Paid)
                throw new ArgumentException("La penalidad ya fue pagada.");

            if (Status == PenaltyStatus.Cancelled)
                throw new ArgumentException("No se puede pagar una penalidad cancelada.");

            Status = PenaltyStatus.Paid;
        }

        public void Cancel()
        {
            if (Status == PenaltyStatus.Paid)
                throw new ArgumentException("No se puede cancelar una penalidad ya pagada.");

            Status = PenaltyStatus.Cancelled;
        }

        public void Disable()
        {
            Delete();
        }

    }
}
