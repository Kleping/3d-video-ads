
namespace Core.Locator
{
    public class SuitableModuleResolver<T> where T : class, IModule
    {
        T _service;
        
        public T Resolve => 
            _service != null ? ModuleLocator.Contains<T>() ? _service : null : _service = ModuleLocator.Resolve<T>();
    }
}