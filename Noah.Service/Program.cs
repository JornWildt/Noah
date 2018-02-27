using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Elfisk.ECS.Core;
using Elfisk.ECS.Core.Implementation;
using Elfisk.ECS.Service;
using log4net;
using log4net.Config;
using Microsoft.Owin;
using Noah.Blueprint;
using Topshelf;
using Reg = Castle.MicroKernel.Registration;

[assembly: OwinStartup(typeof(Noah.Service.OwinStartup))]

namespace Noah.Service
{
  class Program
  {
    static readonly ILog Logger = LogManager.GetLogger((typeof(Program)));

    public static IWindsorContainer CastleContainer { get; set; }


    static void Main(string[] args)
    {
      XmlConfigurator.Configure();
      Logger.Info("**************************************************************************************");
      Logger.Info("Starting Noah service");
      Logger.Info("**************************************************************************************");

      CastleContainer = new WindsorContainer();
      InstallDependencies(CastleContainer);

      NoahInitializer.Initialize(CastleContainer.Resolve<IEntityRepository>());

      HostFactory.Run(c =>
      {
        c.Service(s => new ServiceControlWithErrorHandling(new NoahService(CastleContainer, doRunWebHost: true), Logger));
        c.StartManually();
      });

      Logger.Info("**** Service stopped ****");
    }


    private static void InstallDependencies(IWindsorContainer container)
    {
      container.Register(Reg.Component.For<IEntityRepository>().ImplementedBy<InMemoryEntityRepository>());
      container.Register(Reg.Component.For<IPlayersBus>().ImplementedBy<SignalRPlayersBus>());
      container.Register(Reg.Component.For<IGameLoopEventQueue>().ImplementedBy<GameLoopEventQueue>());

      container.Register(
        Classes.FromAssembly(typeof(NoahInitializer).Assembly)
        .BasedOn<ISystem>()
        .WithService.Base());
    }
  }
}
