using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Game
{
    public static class Constants
    {
        public const string HighScoreKey = "HighScore";

        public static readonly Dictionary<PointValueType, int> PointValues = new()
        {
            { PointValueType.None, 0 },
            { PointValueType.Small, 10 },
            { PointValueType.Medium, 20 },
            { PointValueType.Large, 30 }
        };

        public static readonly Dictionary<PointValueType, Color> PointColors = new()
        {
            { PointValueType.None, Color.clear },
            { PointValueType.Small, Color.green },
            { PointValueType.Medium, Color.yellow },
            { PointValueType.Large, Color.red }
        };
    }
}