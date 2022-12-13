using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class PersonServiceException : Xeption
    {
        public PersonServiceException(Xeption innerException)
            : base(message: "Person service error occured, please fix the problem and try again", innerException)
        { }
    }
}

