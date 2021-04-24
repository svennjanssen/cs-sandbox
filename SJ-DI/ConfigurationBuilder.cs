namespace SJDI {
    public interface IConfigurationBuilder {
        ISpecificationBuilder RegisterType<T>();
    }
    
    public class ConfigurationBuilder : IConfigurationBuilder {
        private readonly RegistrationDefinitionCollection _registrationDefinitionCollection;
        
        public ConfigurationBuilder(RegistrationDefinitionCollection registrationDefinitionCollection)
        {
            _registrationDefinitionCollection = registrationDefinitionCollection;
        }

        public ISpecificationBuilder RegisterType<T>() {
            var registrationDefinition = _registrationDefinitionCollection.Add(new RegistrationDefinition{
                RegisteredType = typeof(T),
                InstanceType = typeof(T)
            });

            return new SpecificationBuilder(registrationDefinition);
        }
    }
}