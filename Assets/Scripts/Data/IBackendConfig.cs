using Modules.Backend;

namespace Data
{
    public interface IBackendConfig
    {
        string GetEndpoint(EndpointType type);
    }
}