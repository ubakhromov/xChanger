using FluentAssertions;
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

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllWhenAllServiceErrorOccursAndLogIt()
        {
            // given
            string exceptionMessage = GetRandomMessage();
            var serviceException = new Exception(exceptionMessage);

            var failePersonServiceException =
                new FailedPersonServiceException(serviceException);

            var expectedPersonServiceException =
                new PersonServiceException(failePersonServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPersons())
                    .Throws(serviceException);

            // when
            Action retrieveAllPersonsAction = () =>
                this.personService.RetrieveAllPersons();

            PersonServiceException actualPersonServiceException =
                Assert.Throws<PersonServiceException>(
                    retrieveAllPersonsAction);

            // then
            actualPersonServiceException.Should().BeEquivalentTo(
                expectedPersonServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPersons(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();            
        }
    }
}
