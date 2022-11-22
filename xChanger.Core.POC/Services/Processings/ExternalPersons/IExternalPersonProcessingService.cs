using System.Collections.Generic;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.ExternalPersons;

namespace xChanger.Core.Services.Processings.ExternalPersons
{
    public interface IExternalPersonProcessingService
    {
        ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonsAsync();
    }
}