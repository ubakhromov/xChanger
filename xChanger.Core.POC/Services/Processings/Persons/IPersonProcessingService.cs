using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Persons;

namespace xChanger.Core.Services.Processings.Persons
{
    public interface IPersonProcessingService
    {
        ValueTask<Person> UpsertPersonAsync(Person person);
    }
}
