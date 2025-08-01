using FurionTest.Common;
using FurionTest.Core;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace FurionTest.Web.Core;

public static class IocServiceCollectionExtensions
{
    public static IServiceCollection AddClassesMatchingInterfaces(this IServiceCollection services)
    {
        var assemblies = App.Assemblies;
        var type = GlobalConfig.RegionType.ToString();

        services.Scan(scan => scan.FromAssemblies(assemblies)
        .AddClasses(classes =>
        {
            classes = classes.Where(t => t.Name.EndsWith("Service")).WithoutAttribute<MultiServiceFilterAttribute>();
        })
        .UsingRegistrationStrategy(RegistrationStrategy.Replace())
        .AsImplementedInterfaces()
        .WithTransientLifetime());

        services.Scan(scan => scan.FromAssemblies(assemblies)
        .AddClasses(classes =>
        {
            classes = classes.WithAttribute<MultiServiceFilterAttribute>(o => o._serviceName.TryValidate(type));
        })
        .UsingRegistrationStrategy(RegistrationStrategy.Replace())
        .AsImplementedInterfaces()
        .WithTransientLifetime());

        return services;
    }
}