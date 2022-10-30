﻿using System.Collections.Generic;
using System.Threading.Tasks;
using xChanger.Core.POC.Models.Foundations.ExternalPersons;

namespace xChanger.Core.POC.Services.Orchestrations.ExternalPersons
{
    public interface IExternalPersonOrchestrationService
    {
        ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonsAsync();
    }
}