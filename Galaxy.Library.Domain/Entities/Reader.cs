using Galaxy.Library.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.Entities
{
    public class Reader :BaseEntity
    {
        //public int ReaderId { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public Email Email { get; private set; } = default!;
        public DocumentNumber DocumentNumber { get; private set; } = default!;
        //public bool IsActive { get; private set; }

        private Reader() { }

        private Reader(string fullName, Email email, DocumentNumber documentNumber)
        {
            if (string.IsNullOrEmpty(fullName)) throw new ArgumentNullException("El nombre no puede ser null o vacio", nameof(fullName));
            if (string.IsNullOrEmpty(email.Value)) throw new ArgumentNullException("El correo electrónico no puede ser null o vacio", nameof(email));
            if (string.IsNullOrEmpty(documentNumber.Value)) throw new ArgumentNullException("El número de documento no puede ser null o vacio", nameof(documentNumber));

            FullName = fullName;
            Email = email;
            DocumentNumber = documentNumber;
        }

        public static Reader Create(string fullName, Email email, DocumentNumber documentNumber) {

            return new Reader
            (
               fullName,
               email,
               documentNumber
            );
        }


        public void UpdateInformation(string fullName, Email email, DocumentNumber documentNumber)
        {
            if (string.IsNullOrEmpty(fullName)) throw new ArgumentNullException("El nombre no puede ser null o vacio", nameof(fullName));
            if (string.IsNullOrEmpty(email.Value)) throw new ArgumentNullException("El correo electrónico no puede ser null o vacio", nameof(email));
            if (string.IsNullOrEmpty(documentNumber.Value)) throw new ArgumentNullException("El número de documento no puede ser null o vacio", nameof(documentNumber));
            FullName = fullName;
            Email = email;
            DocumentNumber = documentNumber;
        }
        public void ValidateCanBorrow(bool hasOverdueLoans)
        {
            if (!IsActive)
                throw new ArgumentException("El lector está inactivo y no puede solicitar préstamos.");

            if (hasOverdueLoans)
                throw new ArgumentException("El lector tiene préstamos vencidos y no puede solicitar nuevos préstamos.");
        }

        public void Disable()
        {
            Delete();

        }

    }
}
