using Microsoft.Data.SqlClient;
using Moq;
using xChanger.Core.Models.Foundations.Persons.Exceptions;
using Xunit;

namespace xChanger.Core.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlError();

            var failedPersonStorageException =
                new FailedPersonStorageException(sqlException);

            var expectedPersonDependencyException =
                new PersonDependencyException(failedPersonStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPersons())
                    .Throws(sqlException);

            //when
            Action retrieveAllGuestAction = () =>
                this.personService.RetrieveAllPersons();

            //then
            Assert.Throws<PersonDependencyException>(
                retrieveAllGuestAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPersons());

            this.loggingBrokerMock.Verify(broker =>
             broker.LogCritical(It.Is(SameExceptionAs(
                 expectedPersonDependencyException))),
                     Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
