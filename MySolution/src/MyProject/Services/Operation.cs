namespace MyProject.Services
{
    public class MyOperationService
    {
        public MyOperationService(
            IOperationTransient transient,
            IOperationScoped scoped,
            IOperationSingleton singleton,
            IOperationSingletonInstance singletonInstance) 
        {
            Transient = transient;
            Scoped = scoped;
            Singleton = singleton;
            SingletonInstance = singletonInstance;            
        }

        public IOperationTransient Transient { get; }
        public IOperationScoped Scoped { get; }
        public IOperationSingleton Singleton { get; }
        public IOperationSingletonInstance SingletonInstance { get; }
    }

    public class MyOperation : IOperationTransient,
                                IOperationScoped,
                                IOperationSingleton,
                                IOperationSingletonInstance
    {         
        public MyOperation() : this(Guid.NewGuid()) 
        { 
        }

        public MyOperation(Guid id)
        {
            OperationId = id;
        }

        public Guid OperationId { get; private set; }
    }

    public interface IMyOperation
    {
        Guid OperationId { get; }
    }

    public interface IOperationTransient : IMyOperation
    { 
    }

    public interface IOperationScoped : IMyOperation 
    {
    }

    public interface IOperationSingleton : IMyOperation
    {
    }

    public interface IOperationSingletonInstance : IMyOperation
    {
    }


}