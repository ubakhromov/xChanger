using System.Collections.Generic;
using System.Threading.Tasks;
using xChanger.Core.Brokers.Sheets;
using xChanger.Core.Models.Foundations.ExternalPersons;

namespace xChanger.Core.Services.Foundations.ExternalPersons
{
    public class ExternalPersonService : IExternalPersonService
    {
        private readonly ISheetBroker sheetBroker;

        public ExternalPersonService(ISheetBroker sheetBroker) =>
            this.sheetBroker = sheetBroker;

        public async ValueTask<List<ExternalPerson>> RetrieveAllExternalPersonsAsync() =>
            await this.sheetBroker.ReadAllExternalPersonsAsync();
    }
}
