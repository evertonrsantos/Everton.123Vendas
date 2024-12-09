using Everton._123Vendas.Domain.Entities;
using Everton._123Vendas.Domain.Interfaces.Notification;
using Everton._123Vendas.Domain.Services.Notification;
using Everton._123Vendas.Infrastructure.Data.Repository;
using Everton._123Vendas.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Everton._123Vendas.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection service)
        {
            //Notification
            service.AddScoped<INotification, NotificationService>();
            service.AddSingleton<IContainer, ServiceProviderProxy>();

            //Dependency Injection of Services and Repositories
            service.AddDependencyByName(typeof(EventPublisherBase<>).Assembly, "Publisher");
            service.AddDependencyByName(typeof(RepositoryContext).Assembly, "Repository");
            service.AddDependencyByName(typeof(EntityBase).Assembly, "Service");
        }

        private static void AddDependencyByName(this IServiceCollection service, Assembly assembly, string endFileName)
        {
            var services = assembly.GetTypes().Where(type =>
            type.GetTypeInfo().IsClass && type.Name.EndsWith(endFileName) &&
            !type.GetTypeInfo().IsAbstract);

            foreach (var serviceType in services)
            {
                var allInterfaces = serviceType.GetInterfaces();
                var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));

                foreach (var iServiceType in mainInterfaces)
                {
                    service.AddScoped(iServiceType, serviceType);
                }
            }
        }
    }
}
