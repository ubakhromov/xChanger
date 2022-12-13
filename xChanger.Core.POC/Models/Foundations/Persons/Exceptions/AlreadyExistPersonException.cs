using System;
using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class AlreadyExistPersonException : Xeption
    {
        public AlreadyExistPersonException(Exception innerException)
            : base(message: "Person already exist", innerException)
        { }
    }
}
