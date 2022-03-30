using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ExceptionHandling.LogManagement;
namespace ExceptionHandling.DependencyResolver
{
    public static class DIResolver
    {
        public static IServiceCollection ExceptionDIResolver(this IServiceCollection services)
        {
            services.AddSingleton<ILogManager, LogManager>();
            return services;
        }
    }
}
