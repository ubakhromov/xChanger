using System.Collections.Generic;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.ExternalPersons;

namespace xChanger.Core.Services.Foundations.ExternalPersons
{
    public interface IExternalPersonService
    {
        ValueTask<List<ExternalPerson>> RetrieveAllExternalPersonsAsync();
    }
}
