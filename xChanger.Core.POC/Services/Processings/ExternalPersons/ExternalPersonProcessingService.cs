using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xChanger.Core.Models.Foundations.ExternalPersons;
using xChanger.Core.Services.Foundations.ExternalPersons;

namespace xChanger.Core.Services.Processings.ExternalPersons
{
    public class ExternalPersonProcessingService : IExternalPersonProcessingService
    {
        private readonly IExternalPersonService externalPersonService;

        public ExternalPersonProcessingService(IExternalPersonService externalPersonService) =>
            this.externalPersonService = externalPersonService;

        public async ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonsAsync()
        {
            var retrievedExternalPersons = await this.externalPersonService.RetrieveAllExternalPersonsAsync();
            List<ExternalPerson> formattedExternalPersons = FormatProperties(retrievedExternalPersons);

            return formattedExternalPersons;
        }

        private List<ExternalPerson> FormatProperties(List<ExternalPerson> retrievedExternalPersons)
        {
            var formattedExternalPersons = retrievedExternalPersons.Select(retrievedPerson =>
                new ExternalPerson()
                {
                    PersonName = retrievedPerson.PersonName,
                    Age = retrievedPerson.Age,
                    PetOne = retrievedPerson.PetOne.Trim().Replace("-", string.Empty),
                    PetOneType = retrievedPerson.PetOneType.Trim().Replace("-", string.Empty),
                    PetTwo = retrievedPerson.PetTwo.Trim().Replace("-", string.Empty),
                    PetTwoType = retrievedPerson.PetTwoType.Trim().Replace("-", string.Empty),
                    PetThree = retrievedPerson.PetThree.Trim().Replace("-", string.Empty),
                    PetThreeType = retrievedPerson.PetThreeType.Trim().Replace("-", string.Empty),
                });

            return formattedExternalPersons.ToList();
        }
    }
}
