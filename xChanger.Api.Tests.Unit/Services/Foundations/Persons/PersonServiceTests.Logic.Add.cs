using FluentAssertions;
using Force.DeepCloner;
using Moq;
using xChanger.Core.Models.Foundations.ExternalPersons;
using xChanger.Core.Models.Foundations.Persons;
using Xunit;

namespace xChanger.Core.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldAddPersonAsync()
        {
            //given
            Person randomPerson = CreateRandomPerson();
            Person inputPerson = randomPerson;
            Person returningPerson = inputPerson;
            Person expectedPerson = returningPerson.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPersonAsync(inputPerson))
                    .ReturnsAsync(returningPerson);

            //when
            Person actualPerson =
                await this.personService.AddPersonAsync(inputPerson);

            //then
            actualPerson.Should().BeEquivalentTo(expectedPerson);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPersonAsync(inputPerson),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
