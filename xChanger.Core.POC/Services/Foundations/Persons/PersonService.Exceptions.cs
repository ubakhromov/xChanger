using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Persons.Exceptions;
using xChanger.Core.Models.Foundations.Persons;
using Xeptions;
using Microsoft.Data.SqlClient;

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
            catch (InvalidPersonException invalidPersonException)
            {
                throw CreateAndLogValidationException(invalidPersonException);
            }
            catch (SqlException sqlException)
            {
                var failedPersonStorageException =
                    new FailedPersonStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedPersonStorageException);
            }
        }

        private PersonValidationException CreateAndLogValidationException(Xeption exception)
        {
            var personValidationException =
                new PersonValidationException(exception);

            this.loggingBroker.LogError(personValidationException);

            return personValidationException;
        }

        private PersonDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var personDependencyException = new PersonDependencyException(exception);
            this.loggingBroker.LogCritical(personDependencyException);

            return personDependencyException;
        }
    }
}
