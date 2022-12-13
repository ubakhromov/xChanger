using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class InvalidPersonException : Xeption
    {
        public InvalidPersonException()
            : base(message: "Invalid person. Please correct the errors and try again.")
        {}
    }
}
