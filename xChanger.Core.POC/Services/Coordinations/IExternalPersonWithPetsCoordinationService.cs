using System.Collections.Generic;
using System.Threading.Tasks;
using xChanger.Core.Models.Orchestrations.PersonPets;

namespace xChanger.Core.Services.Coordinations
{
    public interface IExternalPersonWithPetsCoordinationService
    {
        ValueTask<List<PersonPet>> CoordinateExternalPersons();
    }
}
