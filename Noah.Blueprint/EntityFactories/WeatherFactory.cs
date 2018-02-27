using Elfisk.ECS.Core;
using Noah.Blueprint.Weather;

namespace Noah.Blueprint.EntityFactories
{
  public static class WeatherFactory
  {
    public static Entity BuildWeather()
    {
      EntityId id = EntityId.NewId();

      return new Entity(
        id,
        new IComponent[]
        {
          new WeatherComponent(id)
        });
    }
  }
}
