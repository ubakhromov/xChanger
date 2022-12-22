using FluentAssertions;
using Moq;
using xChanger.Core.Models.Foundations.Persons;
using Xunit;

namespace xChanger.Core.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldRetrievePersonsWithPets()
        {
            //given
            IQueryable<Person> randomPersonsWithPets = CreatedRandomPersons();
            IQueryable<Person> storagePersonsWithPets = randomPersonsWithPets;
            IQueryable<Person> expectedPersonsWithPets = storagePersonsWithPets;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPersonsWithPets())
                    .Returns(storagePersonsWithPets);

            //when
            IQueryable<Person> actualPersons =
                this.personService.RetrieveAllPersonsWithPets();

            //then 
            actualPersons.Should().BeEquivalentTo(expectedPersonsWithPets);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPersonsWithPets(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
