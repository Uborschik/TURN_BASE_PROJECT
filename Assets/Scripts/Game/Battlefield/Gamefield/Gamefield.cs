using Game.DI;
using Game.Services;
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

        public Gamefield(TilemapReaderData tilemapReaderData)
        {
            mapOffsetByX = tilemapReaderData.GFTilemap.cellBounds.position.x;
            mapOffsetByY = tilemapReaderData.GFTilemap.cellBounds.position.y;

            tilemapReader = new(tilemapReaderData.GFTilemap);

            tilemapReader.GenerateNodes(out BaseGamefieldNode[,] mapGrid, out BaseNavigationNode[,] navGrid);
            this.mapGrid = mapGrid;
            this.navGrid = navGrid;

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

        public bool TryGetCentralPositionAtWorldPosition(Vector3 position, out Vector3? center)
        {
            GetXY(position, out var x, out var y);

            if (mapGrid.TryGetValue(x, y, out var node))
            {
                center = new Vector3(node.CenterX, node.CenterY);
                return true;
            }

            center = null;
            return false;
        }

        private void GetXY(Vector3 position, out int x, out int y)
        {
            x = Mathf.FloorToInt(position.x - mapOffsetByX);
            y = Mathf.FloorToInt(position.y - mapOffsetByY);
        }
    }
}