using DataAccess.IRepository;
using DataAccess.IService;
using DataAccess.Repository;
using DataAccess.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Đăng ký các services
        services.AddScoped<IUserService, UserService>();
        // Thêm các service khác ở đây...

        // Đăng ký các repositories
        services.AddScoped<IUserRepository, UserRepository>();
        // Thêm các repository khác ở đây...

        return services;
    }

    // Phương pháp tự động đăng ký tất cả các interfaces và implementations
    public static IServiceCollection AddApplicationServicesAutomatically(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
            .ToList();

        foreach (var type in types)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}");
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, type);
            }
        }

        return services;
    }
}