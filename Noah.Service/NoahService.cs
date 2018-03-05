using Castle.Windsor;
using Elfisk.ECS.Service;
using Topshelf;
using ZimmerBot.Core;
using ZimmerBot.Core.Knowledge;

namespace Noah.Service
{
  public class NoahService : GameService
  {
    public static Bot NoahBot { get; protected set; }

    public static BotHandle NoahBotHandle { get; protected set; }

    protected bool DoClearStore { get; set; }


    public NoahService(IWindsorContainer container, bool doRunWebHost, bool doClearStore)
      : base(container, NoahServiceSettings.GameLoopPeriod, doRunWebHost)
    {
      DoClearStore = doClearStore;
    }


    protected override void Initialize()
    {
      base.Initialize();
      InitializeBot();
    }


    private void InitializeBot()
    {
      ZimmerBotConfiguration.Initialize();

      KnowledgeBase.InitializationMode initMode = DoClearStore
        ? KnowledgeBase.InitializationMode.Clear
        : KnowledgeBase.InitializationMode.RestoreIfExists;

      KnowledgeBase kb = new KnowledgeBase();
      kb.Initialize(initMode);
      kb.LoadFromFiles(NoahServiceSettings.BotSource);
      kb.SetupComplete();

      NoahBot = new Bot(kb);
      NoahBotHandle = NoahBot.Run(new NoahBotEnvironment());
    }


    public override bool Stop(HostControl hostControl)
    {
      base.Stop(hostControl);
      ZimmerBotConfiguration.Shutdown();
      return true;
    }
  }
}
