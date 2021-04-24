using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SJDI {
    public class Initializer {
        private readonly RegistrationDefinitionCollection _registrationDefinitionCollection;
        private readonly IConfigurationBuilder _configurationBuilder;

        public Initializer()
        {
            _registrationDefinitionCollection = new RegistrationDefinitionCollection();
            _configurationBuilder = new ConfigurationBuilder(_registrationDefinitionCollection);

            Initialize();
        }

        private void Initialize(){
            var moduleTypes = typeof(Initializer).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(Module)) && t != typeof(Module));

            foreach(var moduleType in moduleTypes){
                var module = (IModule)Activator.CreateInstance(moduleType);
                module.Load(_configurationBuilder);
            }
        }

        public T Resolve<T>() => (T)Resolve(typeof(T));

        public object Resolve(Type type) {
            var registration = _registrationDefinitionCollection.LastOrDefault(x => x.RegisteredType == type);

            if (registration == null)
                throw new MissingRegistrationException($"Type {type.Name} has not been registered");

            object instance = null;
            var constructorInfos = GetConstructors(type);

            if (constructorInfos.Count() == 0)
                return Activator.CreateInstance(GetInstanceType(type));

            foreach(var constructorInfo in constructorInfos) {
                var args = new List<object>();
                try {
                    foreach(var parameterInfo in constructorInfo.GetParameters()){
                        var parameterInstance = Resolve(parameterInfo.ParameterType);
                        args.Add(parameterInstance);
                    }

                    instance = Activator.CreateInstance(GetInstanceType(type), args);
                } catch (MissingRegistrationException){
                    continue;
                }
            }

            if (instance == null)
                throw new Exception($"Could not create instance of type {type.Name} with the available registrations");
            
            return instance;
        }

        private ConstructorInfo[] GetConstructors(Type type){
            var instanceType = GetInstanceType(type);
            return instanceType.GetConstructors();
        }

        private Type GetInstanceType(Type registeredType) {
            var registration = _registrationDefinitionCollection.Last(x => x.RegisteredType == registeredType);
            return registration.InstanceType;
        }
    }
}