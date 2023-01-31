using DeployAWS.Infrastructure.Settings.NoSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeployAWS.API.Configurations
{
    public static class MongoDBSetup
    {
        public static void AddMongoDBSetup(this IServiceCollection services, IConfiguration Configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.Configure<ProductDatabaseSettings>(Configuration.GetSection("NoSQLConnStrings"));
        }
    }
}
