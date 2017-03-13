using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shared.Algorithms;
using Shared.POCO;

namespace Tests
{
    [TestFixture]
    public class AStarPathSearchTest
    {
        private Position _startPosition;
        private Position _targetPosition;

        private int[,] _map;

        [SetUp]
        public void SetUp()
        {
            _map = new int[5, 5];
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    _map[i, j] = 0;
                }
            }

            _startPosition = new Position(1, 1);
            _targetPosition = new Position(4, 4);
        }

        [Test]
        public void GetPathForNodeTest()
        {
            List<Position> positions = new List<Position>
            {
                new Position(0, 0),
                new Position(0, 1),
                new Position(0, 2)
            };

            PathNode pathNode = new PathNode
            {
                Position = positions[2],
                ParentPoint = new PathNode
                {
                    Position = positions[1],
                    ParentPoint = new PathNode()
                    {
                        Position = positions[0]
                    }
                }
            };

            var path = AStarPathSearch.GetPathForNode(pathNode);

            Assert.AreEqual(positions.Count, path.Count);
            Assert.AreEqual(positions, path);
        }

        [Test]
        public void GetDistanceBetweenNeighborsTest()
        {
            var startPoint = new Position(1, 1);
            var endPoint1 = new Position(1, 0);
            var endPoint2 = new Position(1, 2);

            int len1 = AStarPathSearch.GetDistanceBetweenNeighbors(startPoint, endPoint1);
            int len2 = AStarPathSearch.GetDistanceBetweenNeighbors(startPoint, endPoint2);

            Assert.AreEqual(1, len1);
            Assert.AreEqual(1, len2);
        }

        [Test]
        public void GetHeuristicPathLengthTest()
        {
            var startPoint = new Position(0, 0);
            var endPoint = new Position(5, 10);

            int len = AStarPathSearch.GetHeuristicPathLength(startPoint, endPoint);

            Assert.AreEqual(15, len);
        }

        [Test]
        public void GetNeighborsTest()
        {
            PathNode node = new PathNode
            {
                Position = _startPosition,
                ParentPoint = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = AStarPathSearch.GetHeuristicPathLength(_startPosition, _targetPosition)
            };

            var nodes = AStarPathSearch.GetNeighbors(node, _targetPosition, _map);

            Assert.AreEqual(4, nodes.Count);
        }

        [Test]
        public void FindPathTest()
        {
            var points = AStarPathSearch.FindPath(_map, _startPosition, _targetPosition);
            foreach (var point in points)
            {
                Console.WriteLine($"x: {point.X}, y: {point.Y}");
            }
            Assert.AreEqual(7, points.Count);
            Assert.AreEqual(_targetPosition.X, points[points.Count - 1].X);
            Assert.AreEqual(_targetPosition.Y, points[points.Count - 1].Y);
        }
    }
}
