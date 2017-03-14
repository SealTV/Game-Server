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
        private Position _startPosition2;
        private Position _targetPosition2;
        private Position _startPosition3;
        private Position _targetPosition3;

        private int[,] _openMap;
        private int[,] _openMap2;
        private int[,] _openMap3;
        private int[,] _closeMap1;
        private int[,] _closeMap2;
        private int[,] _closeMap3;

        [SetUp]
        public void SetUp()
        {
            _openMap = new int[,]
            {
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0 }
            };

            _openMap2 = new int[,]
            {
                {0, 0, 0, 0, 0 },
                {0, 1, 1, 1, 0 },
                {0, 0, 0, 1, 0 },
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0 }
            };


            _openMap3 = new int[,]
            {
                {0, 0, 0, 0, 0, 0 },
                {1, 1, 1, 1, 1, 0 },
                {0, 0, 0, 0, 1, 0 },
                {0, 0, 0, 0, 1, 0 },
                {0, 0, 1, 1, 1, 0 },
                {0, 0, 0, 0, 0, 0 }
            };

            _closeMap1 = new int[,]
            {
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 1 },
                {0, 0, 0, 1, 0 }
            };

            _closeMap2 = new int[,]
            {
                {0, 1, 0, 0, 0 },
                {1, 1, 1, 0, 0 },
                {0, 1, 0, 0, 0 },
                {0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0 }
            };

            _closeMap3 = new int[,]
            {
                {1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1 }
            };

            _startPosition = new Position(1, 1);
            _targetPosition = new Position(4, 4);

            _startPosition2 = new Position(2, 2);
            _targetPosition2 = new Position(0, 2);

            _startPosition3 = new Position(3, 3);
            _targetPosition3 = new Position(0, 3);
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

            var nodes = AStarPathSearch.GetNeighbors(node, _targetPosition, _openMap);

            Assert.AreEqual(4, nodes.Count);
        }

        [Test]
        public void FindPath1Test()
        {
            var points = AStarPathSearch.FindPath(_openMap, _startPosition, _targetPosition);
            Assert.AreEqual(7, points.Count);
            Assert.AreEqual(_targetPosition.X, points[points.Count - 1].X);
            Assert.AreEqual(_targetPosition.Y, points[points.Count - 1].Y);
        }

        [Test]
        public void FindPath2Test()
        {
            var points = AStarPathSearch.FindPath(_openMap2, _startPosition2, _targetPosition2);
            Assert.AreEqual(7, points.Count);
            Assert.AreEqual(_targetPosition2.X, points[points.Count - 1].X);
            Assert.AreEqual(_targetPosition2.Y, points[points.Count - 1].Y);
        }

        [Test]
        public void FindPath3Test()
        {
            var points = AStarPathSearch.FindPath(_openMap3, _startPosition3, _targetPosition3);
            Assert.AreEqual(16, points.Count);
            Assert.AreEqual(_targetPosition3.X, points[points.Count - 1].X);
            Assert.AreEqual(_targetPosition3.Y, points[points.Count - 1].Y);
        }

        [Test]
        public void FindPathFaill1Test()
        {
            var points = AStarPathSearch.FindPath(_closeMap1, _startPosition, _targetPosition);
           
            Assert.Null(points);
        }

        [Test]
        public void FindPathFaill2Test()
        {
            var points = AStarPathSearch.FindPath(_closeMap2, _startPosition, _targetPosition);
            Assert.Null(points);
        }

        [Test]
        public void GetNearestPoint1Test()
        {
            var points = AStarPathSearch.GetNearestPoints(_targetPosition, _openMap, 1);

            Assert.NotNull(points);
            Assert.AreEqual(3, points.Count);
        }

        [Test]
        public void GetNearestPoint2Test()
        {
            var points = AStarPathSearch.GetNearestPoints(_targetPosition, _closeMap1, 1);

            Assert.NotNull(points);
            Assert.AreEqual(1, points.Count);
        }

        [Test]
        public void GetNearestPoint3Test()
        {
            var points = AStarPathSearch.GetNearestPoints(_targetPosition, _closeMap1, 2);

            Assert.NotNull(points);
            Assert.AreEqual(5, points.Count);
        }

        [Test]
        public void FindPathToNearestPoint1Test()
        {
            var points = AStarPathSearch.FindPathToNearestPoint(_closeMap1, _startPosition, _targetPosition);

            Assert.NotNull(points);
            Assert.AreEqual(5, points.Count);
        }

        [Test]
        public void FindPathToNearestPoint2Test()
        {
            var points = AStarPathSearch.FindPathToNearestPoint(_closeMap2, _startPosition, _targetPosition);

            Assert.NotNull(points);
            Assert.AreEqual(1, points.Count);
            Assert.AreEqual(_startPosition, points[0]);
        }

        [Test]
        public void FindPathToNearestPoint3Test()
        {
            var points = AStarPathSearch.FindPathToNearestPoint(_closeMap3, _startPosition, _targetPosition);

            Assert.NotNull(points);
            Assert.AreEqual(1, points.Count);
            Assert.AreEqual(_startPosition, points[0]);
        }
    }
}
