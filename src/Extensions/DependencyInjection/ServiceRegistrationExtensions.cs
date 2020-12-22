using Microsoft.Extensions.DependencyInjection;

namespace MS.Extensions.DependencyInjection
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddSingletonExt<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            return services
                .AddSingleton<TImplementation>()
                .AddSingleton<TService, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddSingletonExt<TService1, TService2, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TImplementation : class, TService1, TService2
        {
            return services
                .AddSingletonExt<TService1, TImplementation>()
                .AddSingleton<TService2, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddSingletonExt<TService1, TService2, TService3, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TService3 : class where TImplementation : class, TService1, TService2, TService3
        {
            return services
                .AddSingletonExt<TService1, TService2, TImplementation>()
                .AddSingleton<TService3, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddSingletonExt<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TService3 : class where TService4 : class where TImplementation : class, TService1, TService2, TService3, TService4
        {
            return services
                .AddSingletonExt<TService1, TService2, TService3, TImplementation>()
                .AddSingleton<TService3, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddTransientExt<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            return services
                .AddTransient<TImplementation>()
                .AddTransient<TService, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddTransientExt<TService1, TService2, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TImplementation : class, TService1, TService2
        {
            return services
                .AddTransientExt<TService1, TImplementation>()
                .AddTransient<TService2, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddTransientExt<TService1, TService2, TService3, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TService3 : class where TImplementation : class, TService1, TService2, TService3
        {
            return services
                .AddTransientExt<TService1, TService2, TImplementation>()
                .AddTransient<TService3, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddTransientExt<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TService3 : class where TService4 : class where TImplementation : class, TService1, TService2, TService3, TService4
        {
            return services
                .AddTransientExt<TService1, TService2, TService3, TImplementation>()
                .AddTransient<TService3, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddScopedExt<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            return services
                .AddScoped<TImplementation>()
                .AddScoped<TService, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddScopedExt<TService1, TService2, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TImplementation : class, TService1, TService2
        {
            return services
                .AddScopedExt<TService1, TImplementation>()
                .AddScoped<TService2, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddScopedExt<TService1, TService2, TService3, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TService3 : class where TImplementation : class, TService1, TService2, TService3
        {
            return services
                .AddScopedExt<TService1, TService2, TImplementation>()
                .AddScoped<TService3, TImplementation>(p => p.GetService<TImplementation>());
        }

        public static IServiceCollection AddScopedExt<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection services) where TService1 : class where TService2 : class where TService3 : class where TService4 : class where TImplementation : class, TService1, TService2, TService3, TService4
        {
            return services
                .AddScopedExt<TService1, TService2, TService3, TImplementation>()
                .AddScoped<TService3, TImplementation>(p => p.GetService<TImplementation>());
        }

    }
}
