using System.Threading.Tasks;
using xChanger.Core.Models.Orchestrations.PersonPets;

namespace xChanger.Core.Services.Orchestrations.PersonPets
{
    public interface IPersonPetOrchestrationService
    {
        ValueTask<PersonPet> ProcessPersonWithPetsAsync(PersonPet personPet);
    }
}
