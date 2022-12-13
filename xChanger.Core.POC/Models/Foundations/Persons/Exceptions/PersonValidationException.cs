using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class PersonValidationException : Xeption
    {
        public PersonValidationException(Xeption innerException)
            : base (message: "Profile validation errors occurred, please try again",
                  innerException)
        {}        
    }
}
