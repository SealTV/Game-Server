using Shared.POCO;

namespace Shared.Algorithms
{
    public sealed class PathNode
    {
        /// <summary>
        /// Координаты точки на карте.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Длина пути от старта (G).
        /// </summary>
        public int PathLengthFromStart { get; set; }

        /// <summary>
        /// Точка, из которой пришли в эту точку.
        /// </summary>
        public PathNode ParentPoint { get; set; }

        /// <summary>
        /// Примерное расстояние до цели (H).
        /// </summary>
        public int HeuristicEstimatePathLength { get; set; }

        /// <summary>
        /// Ожидаемое полное расстояние до цели (F).
        /// </summary>
        public int EstimateFullPathLength => PathLengthFromStart + HeuristicEstimatePathLength;
    }
}
