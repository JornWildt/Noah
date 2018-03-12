using System.Threading.Tasks;
using Elfisk.ECS.Core;

// Currently not used, but saved as an example

namespace Noah.Blueprint.Weather
{
  public class WeatherSystem : ISystem
  {
    #region Dependencies

    public IEntityRepository Entities { get; set; }

    #endregion


    public Task Update(GameEnvironment environment)
    {
      /*
      WeatherComponent weather = Entities.GetSingletonComponent<WeatherComponent>();
      weather.Update();
      */

      return Task.CompletedTask;
    }
  }
}
