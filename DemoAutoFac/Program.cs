using Autofac;

namespace DemoAutoFac
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            // Configure
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            builder.RegisterType<DependentClass>().As<DependentClass>();
            Container = builder.Build();

            // Use it
            using (var scope = Container.BeginLifetimeScope())
            {
                var d = scope.Resolve<DependentClass>();
                d.Doit();
            } 

        }

        public static void WriteDate()
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }

    public class DependentClass
    {
        private readonly IDateWriter _dateWriter;

        public DependentClass(IDateWriter dateWriter)
        {
            _dateWriter = dateWriter;
        }

        public void Doit()
        {
            _dateWriter.WriteDate();
        }
    }
}