using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class PersonDependencyValidationException : Xeption
    {
        public PersonDependencyValidationException(Xeption innerException)
            : base(message: "Person dependency validation error occurred, please fix the error and try again",
                  innerException)
        { }
    }
}
