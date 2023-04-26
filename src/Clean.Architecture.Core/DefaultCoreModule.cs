using Autofac;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Clean.Architecture.Core.UserAggregate.Services;

namespace Clean.Architecture.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    

    builder.RegisterType<UserSignupService>()
      .As<IUserSignup>().InstancePerLifetimeScope();
  }
}
