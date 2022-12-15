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
            GridController.Instance.PlaceObject(transform.position, Bounds);

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
            if (path == null) return;
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(TracePath(path));

            IEnumerator TracePath(List<GridTile> path)
            {
                int pathIndex = 0;
                do
                {
                    Vector2 targetPosition = path[pathIndex].worldPosition;
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
                    if (((Vector2)transform.position - targetPosition).sqrMagnitude <= float.Epsilon)
                    {
                        pathIndex++;
                    }
                    yield return null;
                } while (pathIndex < path.Count - 1);
            }
        }
    }
}