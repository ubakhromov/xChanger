using Moq;
using System.Text.RegularExpressions;
using Tynamix.ObjectFiller;
using xChanger.Core.POC.Brokers.Sheets;
using xChanger.Core.POC.Brokers.Storages;
using xChanger.Core.POC.Models.Foundations.ExternalPersons;
using xChanger.Core.POC.Services.Foundations.ExternalPersons;
using Xunit.Abstractions;

namespace xChanger.Core.Tests.Unit.Services.Foundations.ExternalPersons
{    public partial class ExternalPersonServiceTests
    {
        private readonly Mock<ISheetBroker> sheetBrokerMock;
        private readonly IExternalPersonService externalPersonService;

        public ExternalPersonServiceTests()
        {            
            this.sheetBrokerMock = new Mock<ISheetBroker>();

            this.externalPersonService = new ExternalPersonService(
                sheetBroker: this.sheetBrokerMock.Object);
        }

        private static ExternalPerson CreateRandomExternalPerson() =>
          CreateExternalPersonFiller(date: GetRandomDateTimeOffset()).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static Filler<ExternalPerson> CreateExternalPersonFiller(DateTimeOffset date)
        {
            var filler = new Filler<ExternalPerson>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}

