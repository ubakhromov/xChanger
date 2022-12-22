using System.Linq;
using System.Threading.Tasks;
using xChanger.Core.Brokers.Loggings;
using xChanger.Core.Brokers.Storages;
using xChanger.Core.Models.Foundations.Persons;
using xChanger.Core.Services.Foundations.Persons;

namespace xChanger.Core.Services.Foundations.Persons
{
    public partial class PersonService : IPersonService
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

        public ValueTask<Person> AddPersonAsync(Person person) =>
        TryCatch(async () =>
        {
            ValidatePersonOnAdd(person);
            return await this.storageBroker.InsertPersonAsync(person);
        });

        public IQueryable<Person> RetrieveAllPersons() =>
        TryCatch(() =>
        {
            return this.storageBroker.SelectAllPersons();

        });

        public IQueryable<Person> RetrieveAllPersonsWithPets() =>
            this.storageBroker.SelectAllPersonsWithPets();

        public async ValueTask<Person> UpdatePersonAsync(Person person) =>
            await storageBroker.UpdatePersonAsync(person);
    }
}
