using Game.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Game.Gameplay
{
    public sealed class Gamefield
    {
        private readonly int mapOffsetByX;
        private readonly int mapOffsetByY;

        private readonly TilemapReader tilemapReader;
        private readonly AStar pathfinder;

        private readonly IGamefieldNode[,] mapGrid;
        private readonly INavigationNode[,] navGrid;

        private readonly int width;
        private readonly int height;

        public Gamefield(TilemapReaderData tilemapReaderData)
        {
            mapOffsetByX = tilemapReaderData.GFTilemap.cellBounds.position.x;
            mapOffsetByY = tilemapReaderData.GFTilemap.cellBounds.position.y;

            tilemapReader = new(tilemapReaderData.GFTilemap);

            tilemapReader.GenerateNodes(out BaseGamefieldNode[,] mapGrid, out BaseNavigationNode[,] navGrid);
            this.mapGrid = mapGrid;
            this.navGrid = navGrid;

            width = mapGrid.GetLength(0);
            height = mapGrid.GetLength(1);

            pathfinder = new(navGrid);
        }

        public Vector3[] FindPathBetween(Vector3 startPosition, Vector3 goalPosition)
        {
            GetXY(startPosition, out var startX, out var startY);
            GetXY(goalPosition, out var goalX, out var goalY);

            navGrid.TryGetValue(startX, startY, out var startNode);
            navGrid.TryGetValue(goalX, goalY, out var goalNode);

            return pathfinder.FindPath(startNode, goalNode).ToArray();
        }

        public BaseGamefieldNode GetNodeAtWorldPosition(Vector3 position)
        {
            if (TryGetNodeAtWorldPosition(position, out var node))
            {
                return (BaseGamefieldNode)node;
            }

            return null;
        }

        public BaseGamefieldNode[] GetRandomNodesAtRightGridSideOnEmptyNodes(int count)
        {
            var nodes = new BaseGamefieldNode[count];
            var x = width - 1;
            var i = 0;

            while(i < count)
            {
                if (i == height) x--;

                var y = UnityEngine.Random.Range(0, height);

                var node = mapGrid[x, y];

                if (nodes.Contains(node)) continue;

                if (node.IsWalkable)
                {
                    nodes[i] = (BaseGamefieldNode)node;
                    i++;
                }
            }

            return nodes;
        }

        private bool TryGetNodeAtWorldPosition(Vector3 position, out IGamefieldNode node)
        {
            GetXY(position, out var x, out var y);

            if (IsValid(3, x))
            {
                node = mapGrid[x, y];
                return true;
            }

            node = null;
            return false;
        }

        private bool IsValid(int maxX, int x)
        {
            return x >= 0 && x < maxX;
        }

        private void GetXY(Vector3 position, out int x, out int y)
        {
            x = Mathf.FloorToInt(position.x - mapOffsetByX);
            y = Mathf.FloorToInt(position.y - mapOffsetByY);
        }
    }
}