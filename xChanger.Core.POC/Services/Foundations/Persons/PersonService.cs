using System.Linq;
using System.Threading.Tasks;
using xChanger.Core.POC.Brokers.Loggings;
using xChanger.Core.POC.Brokers.Storages;
using xChanger.Core.POC.Models.Foundations.Persons;

namespace xChanger.Core.POC.Services.Foundations.Persons
{
    public class PersonService : IPersonService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public PersonService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Person> AddPersonAsync(Person person) =>
            await storageBroker.AddPersonAsync(person);

        public IQueryable<Person> RetrieveAllPersons() =>
            this.storageBroker.SelectAllPersons();

        public IQueryable<Person> RetrieveAllPersonsWithPets() =>
            this.storageBroker.SelectAllPersonsWithPets();

        public async ValueTask<Person> UpdatePersonAsync(Person person) =>
            await storageBroker.UpdatePersonAsync(person);
    }
}
