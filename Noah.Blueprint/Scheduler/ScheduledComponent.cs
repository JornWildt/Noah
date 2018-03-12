using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using Elfisk.ECS.Core;

namespace Noah.Blueprint.Scheduler
{
  public class ScheduledComponent : Component
  {
    public DateTime NextChangeTime { get; internal set; }

    public IScheduledJob Job { get; internal set; }


    public ScheduledComponent(EntityId entityId, IScheduledJob job)
      : base(entityId)
    {
      Condition.Requires(job, nameof(job)).IsNotNull();

      NextChangeTime = DateTime.Now;
      Job = job;
    }


    public void Update()
    {
      DateTime now = DateTime.Now;
      if (now >= NextChangeTime)
      {
        NextChangeTime = Job.Execute();
      }
    }
  }
}
