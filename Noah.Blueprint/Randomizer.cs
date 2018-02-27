using System;

namespace Noah.Blueprint
{
  public static class Randomizer
  {
    private static Random _Randomizer = new Random();

    public static int Next() => _Randomizer.Next();

    public static int Next(int maxValue) => _Randomizer.Next(maxValue);

    public static int Next(int minValue, int maxValue) => _Randomizer.Next(minValue, maxValue);
  }
}
