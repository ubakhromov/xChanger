using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Pets;

namespace xChanger.Core.Services.Processings.Pets
{
    public interface IPetProcessingService
    {
        ValueTask<Pet> UpsertPetAsync(Pet pet);
    }
}