using Xeptions;

namespace xChanger.Core.Models.Foundations.Persons.Exceptions
{
    public class NullPersonException : Xeption
    {
        public NullPersonException() 
            : base(message: "Person is null")
        {}
    }
}
