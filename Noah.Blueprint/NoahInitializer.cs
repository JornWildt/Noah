using CuttingEdge.Conditions;
using Elfisk.ECS.Core;

namespace Noah.Blueprint
{
  public static class NoahInitializer
  {
    public static void Initialize(IEntityRepository entities)
    {
      Condition.Requires(entities, nameof(entities)).IsNotNull();

      //Entity player = PlayerFactory.BuildPlayer("Borg");
      //entities.AddEntity(player);
    }
  }
}
