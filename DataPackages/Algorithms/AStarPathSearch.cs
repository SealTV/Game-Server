using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shared.POCO;

namespace Shared.Algorithms
{
    public static class AStarPathSearch
    {
        public static List<Position> FindPath(int[,] map, Position start, Position target)
        {
            // Шаг 1.
            var closedSet = new List<PathNode>();
            var openSet = new List<PathNode>();
            // Шаг 2.
            PathNode startNode = new PathNode
            {
                Position = start,
                ParentPoint = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, target)
            };

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // Шаг 3.
                var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();
                // Шаг 4.
                if (currentNode.Position.X == target.X && currentNode.Position.Y == target.Y)
                    return GetPathForNode(currentNode);
                // Шаг 5.
                openSet.Remove(currentNode);
                if(currentNode == null)
                    Console.WriteLine("NULLLLL");
                closedSet.Add(currentNode);
                // Шаг 6.
                foreach (var item in GetNeighbors(currentNode, target, map))
                {
                    // Шаг 7.
                    if (closedSet.Count(node => node.Position.X == item.Position.X && node.Position.Y == item.Position.Y) > 0)
                        continue;

                    var openNode = openSet.FirstOrDefault(node => node.Position.X == item.Position.X && node.Position.Y == item.Position.Y);
                    // Шаг 8.
                    if (openNode == null)
                    {
                        openSet.Add(item);
                    }
                    else if (openNode.PathLengthFromStart > item.PathLengthFromStart)
                    {
                        // Шаг 9.
                        openNode.ParentPoint = currentNode;
                        openNode.PathLengthFromStart = item.PathLengthFromStart;
                    }
                }
            }
            // Шаг 10.
            return null;
        }

        public static Collection<PathNode> GetNeighbors(PathNode pathNode, Position goal, int[,] map)
        {
            var result = new Collection<PathNode>();

            // Соседними точками являются соседние по стороне клетки.
            var neighborPoints = new[]
            {
                new Position(pathNode.Position.X + 1, pathNode.Position.Y),
                new Position(pathNode.Position.X - 1, pathNode.Position.Y),
                new Position(pathNode.Position.X, pathNode.Position.Y + 1),
                new Position(pathNode.Position.X, pathNode.Position.Y - 1)
            };

            foreach (var point in neighborPoints)
            {
                // Проверяем, что не вышли за границы карты.
                if (point.X < 0 || point.X >= map.GetLength(0))
                    continue;

                if (point.Y < 0 || point.Y >= map.GetLength(1))
                    continue;
                
                // Проверяем, что по клетке можно ходить.
                if (map[point.X, point.Y] == 1)
                    continue;

                // Заполняем данные для точки маршрута.
                var neighborNode = new PathNode
                {
                    Position = point,
                    ParentPoint = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart + GetDistanceBetweenNeighbors(pathNode.Position, point),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };

                result.Add(neighborNode);
            }
            return result;
        }

        public static int GetDistanceBetweenNeighbors(Position p1, Position p2)
        {
            int x = p1.X - p2.X;
            int y = p1.Y - p2.Y;

            return x * x + y * y;
        }

        public static int GetHeuristicPathLength(Position from, Position to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        public static List<Position> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Position>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.ParentPoint;
            }
            result.Reverse();
            return result;
        }
    }
}