using System.Linq;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.Persons;

namespace xChanger.Core.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Person> SelectPersonAsync(Person person);
        IQueryable<Person> SelectAllPersons();
        IQueryable<Person> SelectAllPersonsWithPets();
        ValueTask<Person> UpdatePersonAsync(Person person);
    }
}
