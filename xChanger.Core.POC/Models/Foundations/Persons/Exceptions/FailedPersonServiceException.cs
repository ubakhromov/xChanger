using System;
using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class FailedPersonServiceException : Xeption
    {
        public FailedPersonServiceException(Exception innerException)
           : base(message: "Service error occured", innerException)
        { }
    }
}
