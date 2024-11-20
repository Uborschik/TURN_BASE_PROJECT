using Game.Services;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

namespace Game.Gameplay
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

        public void GenerateNodes(out BaseGamefieldNode[,] gfNodes, out BaseNavigationNode[,] navNodes)
        {
            gfNodes = new BaseGamefieldNode[width, height];
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
                        gfNodes[x, y] = new BaseGamefieldNode(pivotPosition, centerPosition);
                        navNodes[x, y] = new BaseNavigationNode(pivotPosition, centerPosition);
                    }
                }
            }

            AddNeighbours(gfNodes, navNodes);
        }

        private void AddNeighbours(BaseGamefieldNode[,] gfNodes, BaseNavigationNode[,] navNodes)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var gfNode = gfNodes[x, y];
                    var navNode = navNodes[x, y];

                    if (gfNode == null) continue;

                    var gfNeighbours = new List<IGamefieldNode>();
                    var navNeighbours = new List<INavigationNode>();

                    foreach (var direction in directions)
                    {
                        var newX = x + direction.x;
                        var newY = y + direction.y;

                        if (gfNodes.TryGetValue(newX, newY, out var gfNeighbour))
                        {
                            gfNeighbours.Add(gfNeighbour);
                        }

                        if (navNodes.TryGetValue(newX, newY, out var navNeighbour))
                        {
                            navNeighbours.Add(navNeighbour);
                        }
                    }

                    gfNode.AddNeighbors(gfNeighbours);
                    navNode.AddNeighbors(navNeighbours);
                }
            }
        }
    }
}