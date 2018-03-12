using System;
using Elfisk.ECS.Core;
using Noah.Blueprint.Scheduler;

namespace Noah.Blueprint.Noah
{
  public class NoahComponent : Component, IScheduledJob
  {
    public NoahComponent(EntityId entityId) 
      : base(entityId)
    {
    }


    public DateTime Execute()
    {
      return DateTime.Now.Add(TimeSpan.FromSeconds(10));
    }
  }
}
