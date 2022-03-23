using Autofac;
using NLayer.API.Filters;

namespace NLayer.API.Modules
{
    public class OtherModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(NotFoundFilter<>)).InstancePerLifetimeScope();
        }
    }
}
