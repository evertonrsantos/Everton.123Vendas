using Everton._123Vendas.API.Mappers;
using Everton._123Vendas.Domain.Interfaces.Notification;
using Everton._123Vendas.Domain.Services.Notification;
using Everton._123Vendas.Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Everton._123Vendas.Tests.DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        private static ServiceCollection _services;

        public static void AddDependencies()
        {
            if(_services == null)
            {
                _services = new ServiceCollection();
                _services.AddHttpContextAccessor();
                _services.AddAutoMapper(typeof(CompraProfile));
                _services.AddScoped<INotification, NotificationService>();
                _services.AddSingleton<IContainer, ServiceProviderProxy>();

                var provider = _services.BuildServiceProvider();
                ServiceLocator.Initialize(provider);
            }
            
        }
    }
}
