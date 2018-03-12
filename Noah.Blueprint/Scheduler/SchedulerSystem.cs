using System.Collections.Generic;
using System.Threading.Tasks;
using Elfisk.ECS.Core;

namespace Noah.Blueprint.Scheduler
{
  public class SchedulerSystem : ISystem
  {
    #region Dependencies

    public IEntityRepository Entities { get; set; }

    #endregion


    public Task Update(GameEnvironment environment)
    {
      IEnumerable<ScheduledComponent> components = Entities.GetComponents<ScheduledComponent>();

      foreach (var c in components)
        c.Update();

      return Task.CompletedTask;
    }
  }
}
