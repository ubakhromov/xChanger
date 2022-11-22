using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Persons.Exceptions;
using xChanger.Core.Models.Foundations.Persons;
using Xeptions;

namespace xChanger.Core.Services.Foundations.Persons
{
    public partial class PersonService
    {      

        private delegate ValueTask<Person> ReturningGuestFunction();

        private async ValueTask<Person> TryCatch(ReturningGuestFunction returningGuestFunction)
        {
            try
            {
                return await returningGuestFunction();
            }
            catch (NullPersonException nullPersonException)
            {
                throw CreateAndLogValidationException(nullPersonException);
            }
        }

        private PersonValidationException CreateAndLogValidationException(Xeption exception)
        {
            var personValidationException =
                new PersonValidationException(exception);

            this.loggingBroker.LogError(personValidationException);

            return personValidationException;
        }
    }
}
