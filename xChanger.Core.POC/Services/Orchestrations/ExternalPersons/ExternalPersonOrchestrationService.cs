using System.Collections.Generic;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.ExternalPersons;
using xChanger.Core.Services.Processings.ExternalPersons;

namespace xChanger.Core.Services.Orchestrations.ExternalPersons
{
    public class ExternalPersonOrchestrationService : IExternalPersonOrchestrationService
    {
        private readonly IExternalPersonProcessingService externalPersonProcessingService;

        public ExternalPersonOrchestrationService(IExternalPersonProcessingService externalPersonProcessingService)
        {
            this.externalPersonProcessingService = externalPersonProcessingService;
        }

        public ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonsAsync() =>
            this.externalPersonProcessingService.RetrieveFormattedExternalPersonsAsync();
    }
}
