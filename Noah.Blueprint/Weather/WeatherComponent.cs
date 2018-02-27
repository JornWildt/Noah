using System;
using Elfisk.ECS.Core;

namespace Noah.Blueprint.Weather
{
  public class WeatherComponent : Component
  {
    public enum WeatherType
    {
      Sunny,
      Cloudy,
      Raining,
      Snowing
    }


    public WeatherType Weather { get; internal set; }

    public int Wind { get; internal set; }

    public int Temperature { get; internal set; }

    public DateTime NextChangeTime { get; internal set; }


    public WeatherComponent(EntityId entityId) 
      : base(entityId)
    {
      Weather = WeatherType.Sunny;
      Wind = 3;
      Temperature = 10;
      NextChangeTime = DateTime.Now;
    }


    public void Update()
    {
      DateTime now = DateTime.Now;
      if (now >= NextChangeTime)
      {
        int direction = Randomizer.Next(3) - 1;
        if (direction == -1 && Weather != WeatherType.Sunny)
          Weather -= 1;
        else if (direction == 1 && Weather != WeatherType.Snowing)
          Weather += 1;

        direction = Randomizer.Next(3) - 1;
        if (direction == -1 && Wind > 1)
          Wind -= 1;
        else if (direction == 1 && Wind < 8)
          Wind += 1;

        int minTemperature = 10;
        int maxTemperature = 30;
        if (now.Month <= 2 || now.Month >= 11)
        {
          minTemperature -= 15;
          maxTemperature -= 15;
        }
        else if (now.Month <= 4 || now.Month >= 9)
        {
          minTemperature -= 10;
          maxTemperature -= 10;
        }

        direction = Randomizer.Next(3) - 1;
        if (direction == -1 && Temperature > minTemperature)
          Temperature -= 1;
        else if (direction == 1 && Temperature < maxTemperature)
          Temperature += 1;

        NextChangeTime = now.Add(TimeSpan.FromMinutes(1));
      }
    }
  }
}
