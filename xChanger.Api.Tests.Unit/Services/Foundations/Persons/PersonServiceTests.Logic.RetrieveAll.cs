using FluentAssertions;
using Moq;
using Xunit.Abstractions;
using Xunit;
using xChanger.Core.Models.Foundations.Persons;

namespace xChanger.Core.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldRetrievePersons()
        {
            //given
            IQueryable<Person> randomPersons = CreatedRandomPersons();
            IQueryable<Person> storagePersons = randomPersons;
            IQueryable<Person> expectedPersons = storagePersons;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPersons())
                    .Returns(storagePersons);

            //when
            IQueryable<Person> actualPersons =
                this.personService.RetrieveAllPersons();

            //then 
            actualPersons.Should().BeEquivalentTo(expectedPersons);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPersons(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
