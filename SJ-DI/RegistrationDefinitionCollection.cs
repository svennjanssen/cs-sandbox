using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SJDI {
    public class RegistrationDefinitionCollection
    {
        private readonly IList<RegistrationDefinition> registrationDefinitions;

        public RegistrationDefinitionCollection()
        {
            registrationDefinitions = new List<RegistrationDefinition>();
        }

        public RegistrationDefinition Add(RegistrationDefinition registrationDefinition)
        {
            //TODO improve this
            registrationDefinitions.Add(registrationDefinition);

            return registrationDefinition;
        }

        public RegistrationDefinition FirstOrDefault(Func<RegistrationDefinition, bool> condition) =>
            registrationDefinitions.FirstOrDefault(condition);

        public IEnumerable<RegistrationDefinition> Where(Func<RegistrationDefinition, bool> condition) =>
            registrationDefinitions.Where(condition);

        public RegistrationDefinition Last(Func<RegistrationDefinition, bool> condition) =>
            registrationDefinitions.Last(condition);

        public RegistrationDefinition LastOrDefault(Func<RegistrationDefinition, bool> condition) =>
            registrationDefinitions.LastOrDefault(condition);
    }
}