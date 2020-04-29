using Prism.Ioc;

namespace Covid19.Infrastructure.Networking.Ioc
{
    public interface IIocRegistry
    {
        void Register(IContainerRegistry containerRegistry);
    }
}