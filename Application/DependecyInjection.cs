using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependecyInjection).Assembly);
        return services;
    }
}