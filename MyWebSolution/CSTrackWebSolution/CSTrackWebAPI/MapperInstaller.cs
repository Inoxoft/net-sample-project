using System;
using AutoMapper;
using Autofac;
using CSTrackWebAPI.Model.DTO;

namespace Cytometrix
{
    public static class MapperInstaller
    {
        private static Func<Type, object> factoryMethod = Activator.CreateInstance;

        public static void Load(ContainerBuilder builder)
        {
            var profileTypes = MapperInstallerHelper.GetTypes();

            builder.Register(c => new MapperConfiguration(cfg =>
                {
                    foreach (var type in profileTypes)
                    {
                        var dto = factoryMethod.Invoke(type);
                        cfg.CreateProfile(type.Name, tt => (dto as IProfileBase).Configure(cfg));
                    }
                })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
