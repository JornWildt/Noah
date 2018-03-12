using Elfisk.ECS.Core;
using Noah.Blueprint.Scheduler;

namespace Noah.Blueprint.Noah
{
  public static class NoahFactory
  {
    public static Entity BuildNoah()
    {
      EntityId id = EntityId.NewId();

      NoahComponent nc = new NoahComponent(id);

      return new Entity(
        id,
        new IComponent[]
        {
          new ScheduledComponent(id,nc),
          nc
        });
    }
  }
}
