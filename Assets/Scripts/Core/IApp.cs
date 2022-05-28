using Data;

namespace Core
{
    public interface IApp
    {
        IBackendConfig BackendConfig { get; }
    }
}