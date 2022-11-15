using Moq;
using Tynamix.ObjectFiller;
using xChanger.Core.POC.Brokers.Storages;
using xChanger.Core.POC.Models.Foundations.ExternalPersons;
using xChanger.Core.POC.Models.Foundations.Persons;
using xChanger.Core.POC.Services.Foundations.Persons;

namespace xChanger.Core.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IPersonService personService;

        public PersonServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.personService = new PersonService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static Person CreateRandomPerson() =>
         CreatePersonFiller(date: GetRandomDateTimeOffset()).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
           new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static Filler<Person> CreatePersonFiller(DateTimeOffset date)
        {
            var filler = new Filler<Person>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
