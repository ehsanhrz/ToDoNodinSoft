using Autofac;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.Services;
using Clean.Architecture.Core.ToDoAggregate.Interfaces;
using Clean.Architecture.Core.ToDoAggregate.Services;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Clean.Architecture.Core.UserAggregate.Services;

namespace Clean.Architecture.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {


    builder.RegisterType<UserSignupService>()
      .As<IUserSignup>().InstancePerLifetimeScope();

    builder.RegisterType<TokenProviderService>()
      .As<ITokenProvider>().InstancePerLifetimeScope();

    builder.RegisterType<UserLoginService>()
      .As<IUserLogin>().InstancePerLifetimeScope();

    builder.RegisterType<ToDoCrud>()
      .As<IToDoCRUD>().InstancePerLifetimeScope();
    
  }
}
