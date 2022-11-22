using FluentAssertions;
using Moq;
using xChanger.Core.Models.Foundations.Persons.Exceptions;
using xChanger.Core.Models.Foundations.Persons;
using Xunit;
using Xunit.Abstractions;

namespace xChanger.Core.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPersonIsNullAndLogItAsync()
        {
            // given
            Person invalidPerson = null;

            var nullPersonException =
                new NullPersonException();

            var expectedPersonValidationException =
                new PersonValidationException(nullPersonException);

            // when
            ValueTask<Person> addPersonTask =
                this.personService.AddPersonAsync(invalidPerson);

            PersonValidationException actualPersonValidationException =
                await Assert.ThrowsAsync<PersonValidationException>(
                    addPersonTask.AsTask);

            // then
            actualPersonValidationException.Should().BeEquivalentTo(
                expectedPersonValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.AddPersonAsync(invalidPerson),
                    Times.Never);
            
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfPersonIsInvalidAndLogItAsync(
            string invalidText)
        {
            //given            
            var invalidPerson = new Person
            {
                Name = invalidText
            };

            var invalidPersonException = new InvalidPersonException();

            invalidPersonException.AddData(
                key: nameof(Person.Id),
                values: "Id is required");

            invalidPersonException.AddData(
                key: nameof(Person.Name),
                values: "Text is required");
                     

            var expectedPersonValidationException =
                new PersonValidationException(invalidPersonException);

            //when
            ValueTask<Person> addPersonTask =
                this.personService.AddPersonAsync(invalidPerson);

            PersonValidationException actualPersonValidationException =
                await Assert.ThrowsAsync<PersonValidationException>(() =>
                    addPersonTask.AsTask());


            //then
            actualPersonValidationException.Should().BeEquivalentTo(
                expectedPersonValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.AddPersonAsync(It.IsAny<Person>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

    }
}
