using DeployAWS.Application.Dtos;
using DeployAWS.Application.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeployAWS.API.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IValidator<ClientDto>, ClientDtoValidator>();
            services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();
        }
    }
}
