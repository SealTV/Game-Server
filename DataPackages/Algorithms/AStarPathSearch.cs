using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shared.POCO;

namespace Shared.Algorithms
{
    public static class AStarPathSearch
    {
        public static List<Position> FindPathToNearestPoint(int[,] originMap, Position start, Position target)
        {
            List<Position> result = null;
            int[,] tempMap = new int[originMap.GetLength(0), originMap.GetLength(1)];

            for (int i = 0; i < originMap.GetLength(0); i++)
            {
                for (int j = 0; j < originMap.GetLength(1); j++)
                {
                    tempMap[i, j] = originMap[i, j];
                }
            }

            tempMap[target.X, target.Y] = 1;

            int radius = 1;

            List<Position> points = null;

            do
            {
                points = GetNearestPoints(target, tempMap, radius);
                points = points.OrderBy(p => GetHeuristicPathLength(p, target)).ToList();
                radius++;

                foreach (var point in points)
                {
                    result = FindPath(originMap, start, point);

                    if (result != null)
                        return result;

                    tempMap[point.X, point.Y] = 1;
                }


            } while (result == null && radius < originMap.GetLength(0));

            return new List<Position> { start };
        }

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
                var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();

                if (currentNode.Position.X == target.X && currentNode.Position.Y == target.Y)
                    return GetPathForNode(currentNode);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var item in GetNeighbors(currentNode, target, map))
                {
                    if (closedSet.Count(node => node.Position == item.Position) > 0)
                        continue;

                    var openNode = openSet.FirstOrDefault(node => node.Position == item.Position);
                    if (openNode == null)
                    {
                        openSet.Add(item);
                    }
                    else if (openNode.PathLengthFromStart > item.PathLengthFromStart)
                    {
                        openNode.ParentPoint = currentNode;
                        openNode.PathLengthFromStart = item.PathLengthFromStart;
                    }
                }
            }
            return null;
        }

        public static Collection<PathNode> GetNeighbors(PathNode pathNode, Position goal, int[,] map)
        {
            var result = new Collection<PathNode>();

            var neighborPoints = new[]
            {
                new Position(pathNode.Position.X + 1, pathNode.Position.Y),
                new Position(pathNode.Position.X - 1, pathNode.Position.Y),
                new Position(pathNode.Position.X, pathNode.Position.Y + 1),
                new Position(pathNode.Position.X, pathNode.Position.Y - 1)
            };

            foreach (var point in neighborPoints)
            {
                if (point.X < 0 || point.X >= map.GetLength(0))
                    continue;

                if (point.Y < 0 || point.Y >= map.GetLength(1))
                    continue;
                
                if (map[point.X, point.Y] == 1)
                    continue;

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

        public static List<Position> GetNearestPoints(Position position, int[,] map, int radius)
        {
            List < Position > result = new List<Position>();
            for (int i = position.X - radius; i < position.X + radius; i++)
            {
                for (int j = position.Y - radius; j < position.Y + radius; j++)
                {
                    if(position.X == i && position.Y == j || 
                        (Math.Abs(position.X - i) < radius && Math.Abs(position.Y - j) < radius))
                        continue;

                    if (i >= 0 && i < map.GetLength(0) && j >= 0 && j < map.GetLength(1) && map[i, j] == 0)
                    {
                        result.Add(new Position(i, j));
                    }
                }
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