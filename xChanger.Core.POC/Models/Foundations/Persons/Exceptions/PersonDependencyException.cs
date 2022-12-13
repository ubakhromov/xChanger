using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class PersonDependencyException : Xeption
    {
        public PersonDependencyException(Xeption innerException)
            : base(message: "Person dependency error occured, contact support",
                  innerException)
        { }
    }
}
