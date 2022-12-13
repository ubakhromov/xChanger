using System.Collections.Generic;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.ExternalPersons;

namespace xChanger.Core.Brokers.Sheets
{
    public partial interface ISheetBroker
    {
        ValueTask<List<ExternalPerson>> ReadAllExternalPersonsAsync();
    }
}
