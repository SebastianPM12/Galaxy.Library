using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.ValueObject
{
    public record class LoanPeriod
    {
        public DateTime LoanDate { get; }
        public DateTime DueDate { get; }


        private LoanPeriod() { }


        private LoanPeriod(DateTime loanDate, DateTime dueDate)
        {
            if (dueDate <= loanDate)
                throw new ArgumentException("La fecha de devolución debe ser posterior a la fecha de préstamo.", nameof(loanDate)+"-"+nameof(dueDate));

            LoanDate = loanDate;
            DueDate = dueDate;
        }

        public static LoanPeriod Create(DateTime loanDate, DateTime dueDate)
        {
            return new LoanPeriod(loanDate, dueDate);
        }

    }
}
