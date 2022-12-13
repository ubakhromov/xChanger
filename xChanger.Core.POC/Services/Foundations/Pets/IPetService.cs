using System.Linq;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Pets;

namespace xChanger.Core.Services.Foundations.Pets
{
    public interface IPetService
    {
        ValueTask<Pet> AddPetAsync(Pet pet);
        IQueryable<Pet> RetrieveAllPets();
        ValueTask<Pet> UpdatePetAsync(Pet pet);
    }
}