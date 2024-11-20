using System.Collections.Generic;
using UnityEngine;

namespace Game.Services
{
    public class AStar
    {
        private readonly INavigationNode[,] nodes;

        private const int DIAGONAL_COST = 14;
        private const int GENERAL_COST = 10;

        public AStar(INavigationNode[,] nodes) => this.nodes = nodes;

        public IEnumerable<Vector3> FindPath(INavigationNode startNode, INavigationNode endNode)
        {
            if (!startNode.IsWalkable || !endNode.IsWalkable || startNode == endNode) return null;
            else
            {
                var openSet = new Heap<INavigationNode>(nodes.Length);
                var closedSet = new HashSet<INavigationNode>();

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    var node = openSet.RemoveFirst();

                    closedSet.Add(node);

                    if (node == endNode)
                    {
                        var path = RetracePath(startNode, endNode);

                        return path;
                    }

                    foreach (var neighbour in node.Neighbours)
                    {
                        if (neighbour == null) continue;
                        if (!neighbour.IsWalkable) continue;
                        if (closedSet.Contains(neighbour)) continue;

                        var newCostToNeighbour = node.G + GetDistance(node, neighbour);

                        if (newCostToNeighbour < neighbour.G || !openSet.Contains(neighbour))
                        {
                            neighbour.G = newCostToNeighbour;
                            neighbour.H = GetDistance(neighbour, endNode);
                            neighbour.Parent = node;

                            if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                            else openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }

            return null;
        }

        private IEnumerable<Vector3> RetracePath(INavigationNode startNode, INavigationNode endNode)
        {
            var path = new List<Vector3>();
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                var position = new Vector3(currentNode.CenterPosition.x, currentNode.CenterPosition.y);

                path.Add(position);
                currentNode = currentNode.Parent;
            }

            path.Reverse();
            return path;
        }

        private int GetDistance(INavigationNode a, INavigationNode b)
        {
            var dstX = Mathf.Abs(a.PivotPosition.x - b.PivotPosition.x);
            var dstY = Mathf.Abs(a.PivotPosition.y - b.PivotPosition.y);

            if (dstX > dstY)
                return DIAGONAL_COST * dstY + GENERAL_COST * (dstX - dstY);
            return DIAGONAL_COST * dstX + GENERAL_COST * (dstY - dstX);
        }
    }
}