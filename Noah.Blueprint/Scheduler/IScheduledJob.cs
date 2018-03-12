using System;

namespace Noah.Blueprint.Scheduler
{
  public interface IScheduledJob
  {
    DateTime Execute();
  }
}
