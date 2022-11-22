using System.Collections.Generic;
using xChanger.Core.Models.Foundations.Persons;
using xChanger.Core.Models.Foundations.Pets;

namespace xChanger.Core.Models.Orchestrations.PersonPets
{
    public class PersonPet
    {
        public Person Person { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
