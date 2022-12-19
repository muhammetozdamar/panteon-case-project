using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class Pathfinder
    {
        public enum PathfinderFallback
        {
            FindClosestAvailableTile = 0,
            ReturnNullPath = 1,
        }
        private Grid grid;
        public Pathfinder(Grid grid)
        {
            this.grid = grid;
        }
        public List<GridTile> GetPath(Vector2 startPosition, Vector2 endPosition, PathfinderFallback fallback = PathfinderFallback.FindClosestAvailableTile)
        {
            GridTile startTile = grid.GetTileFromWorldPosition(startPosition);
            GridTile endTile = grid.GetTileFromWorldPosition(endPosition);
            if (startTile == endTile) return null;
            if (endTile.isOccupied)
            {
                if (fallback == PathfinderFallback.FindClosestAvailableTile)
                    endTile = grid.FindClosestAvailableTile(endTile);
                else if (fallback == PathfinderFallback.ReturnNullPath)
                    return null;
            }

            List<GridTile> openSet = new List<GridTile>();
            HashSet<GridTile> closedSet = new HashSet<GridTile>();
            openSet.Add(startTile);

            while (openSet.Count > 0)
            {
                GridTile tile = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].Info.fCost <= tile.Info.fCost)
                    {
                        if (openSet[i].Info.hCost < tile.Info.hCost)
                            tile = openSet[i];
                    }
                }

                openSet.Remove(tile);
                closedSet.Add(tile);

                if (tile == endTile)
                {
                    return RetracePath(startTile, endTile);
                }

                foreach (GridTile neighbour in grid.GetNeighbours(tile))
                {
                    if (neighbour.isOccupied || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = tile.Info.gCost + grid.GetDistance(tile, neighbour);
                    if (newCostToNeighbour < neighbour.Info.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.Info.gCost = newCostToNeighbour;
                        neighbour.Info.hCost = grid.GetDistance(neighbour, endTile);
                        neighbour.Info.parent = tile;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
            return null;
        }


        private List<GridTile> RetracePath(GridTile startNode, GridTile endNode)
        {
            List<GridTile> path = new List<GridTile>();
            GridTile currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Info.parent;
            }
            path.Reverse();
            return path;
        }
    }
}