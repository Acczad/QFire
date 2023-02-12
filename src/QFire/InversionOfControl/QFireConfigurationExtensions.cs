using QFire.Abstraction.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QFireConfigurationExtensions
    {
        public static QFireConfiguration 
            WithRedis(
            this QFireConfiguration options,Action<QFireConfiguration> qFireConfiguration)
        {
            qFireConfiguration(options);
            return options;
        }
    }
}
