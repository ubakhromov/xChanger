using Microsoft.Data.SqlClient;
using Moq;
using Xunit;
using xChanger.Core.Models.Foundations.Persons;
using xChanger.Core.Models.Foundations.Persons.Exceptions;

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

    }
}
