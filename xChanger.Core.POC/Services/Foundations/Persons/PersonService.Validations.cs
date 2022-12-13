using System;
using System.Runtime.CompilerServices;
using xChanger.Core.Models.Foundations.Persons.Exceptions;
using xChanger.Core.Models.Foundations.Persons;

namespace xChanger.Core.Services.Foundations.Persons
{
    public partial class PersonService
    {
        private void ValidatePersonOnAdd(Person person)
        {
            ValidatePersonIsNotNull(person);

            Validate(
                (Rule: IsInvalid(person.Id), Parameter: nameof(Person.Id)),
                (Rule: IsInvalid(person.Name), Parameter: nameof(Person.Name)));
        }

      
        private void ValidatePersonIsNotNull(Person person)
        {
            if (person is null)
            {
                throw new NullPersonException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };
        
        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };        

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPersonException =
                new InvalidPersonException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPersonException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidPersonException.ThrowIfContainsErrors();
        }
    }
}
