namespace SJDI {
    public interface IModule {
        public void Load(IConfigurationBuilder builder);
    }
    public abstract class Module : IModule {
        public abstract void Load(IConfigurationBuilder builder);
    }
}