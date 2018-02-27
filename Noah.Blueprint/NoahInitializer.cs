using CuttingEdge.Conditions;
using Elfisk.ECS.Core;
using Noah.Blueprint.EntityFactories;

namespace Noah.Blueprint
{
  public static class NoahInitializer
  {
    public static void Initialize(IEntityRepository entities)
    {
      Condition.Requires(entities, nameof(entities)).IsNotNull();

      entities.AddEntity(WeatherFactory.BuildWeather());
    }
  }
}
