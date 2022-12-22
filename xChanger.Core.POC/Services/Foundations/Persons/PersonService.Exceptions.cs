using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Persons.Exceptions;
using xChanger.Core.Models.Foundations.Persons;
using Xeptions;
using Microsoft.Data.SqlClient;
using EFxceptions.Models.Exceptions;
using System;
using System.Linq;

namespace xChanger.Core.Services.Foundations.Persons
{
    public partial class PersonService
    {      

        private delegate ValueTask<Person> ReturningPersonFunction();
        private delegate IQueryable<Person> ReturningPersonsFunction();

        private async ValueTask<Person> TryCatch(ReturningPersonFunction returningPersonFunction)
        {
            try
            {
                return await returningPersonFunction();
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
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistGuestException =
                    new AlreadyExistPersonException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistGuestException);
            }
            catch (Exception exception)
            {
                var failedPersonServiceException =
                    new FailedPersonServiceException(exception);

                throw CreateAndLogServiceException(failedPersonServiceException);
            }
        }

        private IQueryable<Person> TryCatch(ReturningPersonsFunction returningPersonsFunction)
        {
            try
            {
                return returningPersonsFunction();
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

        private PersonDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var personDependencyValidationException = new PersonDependencyValidationException(exception);
            this.loggingBroker.LogError(personDependencyValidationException);

            return personDependencyValidationException;

        }

        private PersonServiceException CreateAndLogServiceException(Xeption exception)
        {
            var personServiceException = new PersonServiceException(exception);
            this.loggingBroker.LogError(personServiceException);

            return personServiceException;
        }
    }
}
