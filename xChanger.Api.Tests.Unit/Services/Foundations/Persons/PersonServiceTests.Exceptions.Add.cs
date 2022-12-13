using Microsoft.Data.SqlClient;
using Moq;
using Xunit;
using xChanger.Core.Models.Foundations.Persons;
using xChanger.Core.Models.Foundations.Persons.Exceptions;
using EFxceptions.Models.Exceptions;
using Xunit.Abstractions;

namespace xChanger.Core.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Person somePerson = CreateRandomPerson();
            SqlException sqlException = GetSqlError();
            var failedPersonStorageException = new FailedPersonStorageException(sqlException);

            var expectedPersonDependencyException =
                new PersonDependencyException(failedPersonStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPersonAsync(somePerson))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Person> addPersonTask =
                this.personService.AddPersonAsync(somePerson);

            //then
            await Assert.ThrowsAsync<PersonDependencyException>(() =>
                addPersonTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPersonAsync(somePerson),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPersonDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            //given
            Person somePerson = CreateRandomPerson();
            string someMessage = GetRandomString();

            DuplicateKeyException duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistPersonException =
                new AlreadyExistPersonException(duplicateKeyException);

            var excpectedPersonDependencyValidationException =
                new PersonDependencyValidationException(alreadyExistPersonException);


            this.storageBrokerMock.Setup(broker =>
                broker.SelectPersonAsync(somePerson))
                    .ThrowsAsync(duplicateKeyException);

            //when 
            ValueTask<Person> addGuestTask =
                this.personService.AddPersonAsync(somePerson);

            //then
            await Assert.ThrowsAsync<PersonDependencyValidationException>(() =>
                addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPersonAsync(somePerson),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    excpectedPersonDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            Person somePerson = CreateRandomPerson();
            var serviceException = new Exception();

            var failedPersonServiceException =
                new FailedPersonServiceException(serviceException);

            var expectedPersonServiceException =
                new PersonServiceException(failedPersonServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPersonAsync(somePerson))
                    .ThrowsAsync(serviceException);

            //when
            ValueTask<Person> addPersonTask =
                this.personService.AddPersonAsync(somePerson);

            //then
            await Assert.ThrowsAsync<PersonServiceException>(() =>
                addPersonTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPersonAsync(somePerson),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
