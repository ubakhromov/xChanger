using System.Linq;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Persons;

namespace xChanger.Core.Services.Foundations.Persons
{
    public interface IPersonService
    {
        ValueTask<Person> AddPersonAsync(Person person);
        IQueryable<Person> RetrieveAllPersons();
        IQueryable<Person> RetrieveAllPersonsWithPets();
        ValueTask<Person> UpdatePersonAsync(Person person);
    }
}