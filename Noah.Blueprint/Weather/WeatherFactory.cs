using Elfisk.ECS.Core;
using Noah.Blueprint.Scheduler;
using Noah.Blueprint.Weather;

namespace Noah.Blueprint.Weather
{
  public static class WeatherFactory
  {
    public static Entity BuildWeather()
    {
      EntityId id = EntityId.NewId();

      WeatherComponent wc = new WeatherComponent(id);

      return new Entity(
        id,
        new IComponent[]
        {
          new ScheduledComponent(id,wc),
          wc
        });
    }
  }
}
