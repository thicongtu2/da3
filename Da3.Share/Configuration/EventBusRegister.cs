using System;
using JKang.EventBus.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Da3.Share.Configuration
{
    public static class EventBusRegister
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services,
            Action<IEventBusBuilder> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            IEventBusBuilder builder = new EventBusBuilder(services);

            setupAction?.Invoke(builder);

            return services;
        }
    }
}