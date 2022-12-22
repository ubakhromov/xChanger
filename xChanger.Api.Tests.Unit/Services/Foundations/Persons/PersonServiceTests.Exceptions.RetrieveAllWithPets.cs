using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Persons.Exceptions;
using Xunit;

namespace xChanger.Core.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWithPetsWhenSqlExceptionOccursAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlError();

            var failedPersonStorageException =
                new FailedPersonStorageException(sqlException);

            var expectedPersonDependencyException =
                new PersonDependencyException(failedPersonStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPersonsWithPets())
                    .Throws(sqlException);

            //when
            Action retrieveAllPersonWithPetsAction = () =>
                this.personService.RetrieveAllPersonsWithPets();

            //then
            Assert.Throws<PersonDependencyException>(
                retrieveAllPersonWithPetsAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPersonsWithPets());

            this.loggingBrokerMock.Verify(broker =>
             broker.LogCritical(It.Is(SameExceptionAs(
                 expectedPersonDependencyException))),
                     Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
