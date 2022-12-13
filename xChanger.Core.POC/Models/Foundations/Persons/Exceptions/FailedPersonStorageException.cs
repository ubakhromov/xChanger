using System;
using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class FailedPersonStorageException : Xeption
    {
        public FailedPersonStorageException(Exception innerException)
           : base(message: "Failed person storage error occured, contact support",
                 innerException)
        { }
    }
}
