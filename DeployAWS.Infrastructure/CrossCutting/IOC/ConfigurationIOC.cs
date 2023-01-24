using Autofac;
using AutoMapper;
using DeployAWS.Application;
using DeployAWS.Application.Interfaces;
using DeployAWS.Application.Mappers;
using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Services;
using DeployAWS.Infrastructure.Data.Repositorys;

namespace DeployAWS.Infrastructure.CrossCutting.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region IOC

            builder.RegisterType<ApplicationServiceClient>().As<IApplicationServiceClient>();
            builder.RegisterType<ApplicationServiceProduct>().As<IApplicationServiceProduct>();
            builder.RegisterType<ServiceClient>().As<IServiceClient>();
            builder.RegisterType<ServiceProduct>().As<IServiceProduct>();
            builder.RegisterType<RepositoryClient>().As<IRepositoryClient>();
            builder.RegisterType<RepositoryProduct>().As<IRepositoryProduct>();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelMappingClient());
                cfg.AddProfile(new ModelToDtoMappingClient());
                cfg.AddProfile(new DtoToModelMappingProduct());
                cfg.AddProfile(new ModelToDtoMappingProduct());

            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();

            #endregion IOC
        }
    }

}