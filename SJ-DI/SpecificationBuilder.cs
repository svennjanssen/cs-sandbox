namespace SJDI {
    public interface ISpecificationBuilder {
        ISpecificationBuilder As<T>();
    }

    public class SpecificationBuilder : ISpecificationBuilder
    {
        private RegistrationDefinition RegistrationDefinition;

        public SpecificationBuilder(RegistrationDefinition registrationDefinition)
        {
            RegistrationDefinition = registrationDefinition;
        }
        public ISpecificationBuilder As<T>()
        {
            RegistrationDefinition.RegisteredType = typeof(T);

            return this;
        }
    }
}