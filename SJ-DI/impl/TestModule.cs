using SJDI;

namespace SJDI.Impl {
    public class TestModule : Module
    {
        public override void Load(IConfigurationBuilder builder)
        {
            builder.RegisterType<ClassA>().As<IClassA>();
            builder.RegisterType<ClassB>();
        }
    }

    public interface IClassA {}
    public class ClassA : IClassA {}
    public class ClassB {
        private readonly IClassA _classA;
        public ClassB(IClassA classA)
        {
            _classA = classA;
        }
    }
}