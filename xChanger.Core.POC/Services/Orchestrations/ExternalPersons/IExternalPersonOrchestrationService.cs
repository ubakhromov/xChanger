using System.Collections.Generic;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.ExternalPersons;

namespace xChanger.Core.Services.Orchestrations.ExternalPersons
{
    public interface IExternalPersonOrchestrationService
    {
        ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonsAsync();
    }
}