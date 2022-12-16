using System.Collections;
using System.Collections.Generic;
using BaridaGames.PanteonCaseProject.Gameplay.Astar;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class RegularSoldier : SoldierBase
    {
        private Coroutine moveCoroutine;
        private GridTile currentTile;
        internal override void Start()
        {
            base.Start();
            currentTile = GridController.Instance.GetTile(transform.position);
            currentTile.isOccupied = true;
        }
        public override void Attack(UnitBase targetUnit)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanAttack(UnitBase targetUnit)
        {
            // can add spesific checks later if needed.
            return true;
        }

        public override bool CanMove(Vector2 targetPosition)
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Vector2 targetPosition)
        {
            List<GridTile> path = GridController.Instance.GetPath(transform.position, targetPosition);
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            if (path == null) return;
            currentTile.isOccupied = false;
            moveCoroutine = StartCoroutine(TracePath(path));
            IEnumerator TracePath(List<GridTile> path)
            {
                int pathIndex = 0;
                do
                {
                    currentTile = path[pathIndex];
                    currentTile.isOccupied = true;
                    Vector2 targetPosition = currentTile.worldPosition;
                    if (((Vector2)transform.position - targetPosition).sqrMagnitude <= float.Epsilon)
                    {
                        if (pathIndex < path.Count - 1)
                        {
                            currentTile.isOccupied = false;
                            pathIndex++;
                            currentTile = path[pathIndex];
                            currentTile.isOccupied = true;
                        }
                        else
                        {
                            pathIndex++;
                        }
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
                    }
                    yield return null;
                } while (pathIndex < path.Count);
            }
        }
    }
}