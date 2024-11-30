using Game.Services;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

namespace Game.Gameplay.Gamefields
{
    public class TilemapReader
    {
        private readonly Tilemap tilemap;
        private readonly BoundsInt bounds;

        private readonly int width;
        private readonly int height;
        private readonly float halfCellSizeX;
        private readonly float halfCellSizeY;

        private readonly Vector3Int[] directions;

        public TilemapReader(Tilemap tilemap)
        {
            tilemap.CompressBounds();

            this.tilemap = tilemap;
            bounds = tilemap.cellBounds;

            width = bounds.size.x;
            height = bounds.size.y;

            halfCellSizeX = tilemap.cellSize.x * 0.5f;
            halfCellSizeY = tilemap.cellSize.y * 0.5f;

            directions = Directions2D.eightDirections;
        }

        public void GenerateNodes(out BaseNavigationNode[,] navNodes)
        {
            navNodes = new BaseNavigationNode[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pivotX = x + bounds.position.x;
                    var pivotY = y + bounds.position.y;
                    var pivotPosition = new Vector3Int(pivotX, pivotY);

                    var centerX = pivotX + halfCellSizeX;
                    var centerY = pivotY + halfCellSizeY;
                    var centerPosition = new Vector3(centerX, centerY);

                    if(tilemap.GetTile(pivotPosition))
                    {
                        navNodes[x, y] = new BaseNavigationNode(pivotPosition, centerPosition);
                    }
                }
            }

            AddNeighbours(navNodes);
        }

        private void AddNeighbours(BaseNavigationNode[,] navNodes)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var navNode = navNodes[x, y];

                    var navNeighbours = new List<INavigationNode>();

                    foreach (var direction in directions)
                    {
                        var newX = x + direction.x;
                        var newY = y + direction.y;

                        if (navNodes.TryGetValue(newX, newY, out var navNeighbour))
                        {
                            navNeighbours.Add(navNeighbour);
                        }
                    }

                    navNode.AddNeighbors(navNeighbours);
                }
            }
        }
    }
}