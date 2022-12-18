using System.Collections;
using System.Collections.Generic;
using BaridaGames.PanteonCaseProject.Gameplay.Astar;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class RegularSoldier : SoldierBase
    {
        private Coroutine moveCoroutine;
        private Coroutine attackCoroutine;

        private GridTile currentTile;
        private bool isMoving = false;

        internal override void Start()
        {
            base.Start();
            currentTile = GridController.Instance.GetTile(transform.position);
            currentTile.isOccupied = true;
        }

        public override bool CanMove(Vector2 targetPosition)
        {
            // can add checks if needed, but pathfinding handles the unreachable positions with fallback
            return true;
        }

        public override void Move(Vector2 targetPosition)
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            List<GridTile> path = GridController.Instance.GetPath(transform.position, targetPosition);
            if (path == null) return;

            currentTile.isOccupied = false;
            moveCoroutine = StartCoroutine(TracePath(path));

            IEnumerator TracePath(List<GridTile> path)
            {
                isMoving = true;
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
                isMoving = false;
            }
        }

        public override bool CanAttack(UnitBase target)
        {
            // can add checks if needed, ex. cannot attack same team
            return true;
        }

        public override void Attack(UnitBase target)
        {
            StopAllCoroutines();
            StartCoroutine(Attack(target));
            IEnumerator Attack(UnitBase target)
            {
                Vector2 targetPosition = target.transform.position;
                float range = AttackRange * AttackRange;
                if ((targetPosition - (Vector2)transform.position).sqrMagnitude > range)
                {
                    Move(targetPosition);
                    yield return new WaitUntil(() => !isMoving);
                }

                WaitForSeconds attackTimer = new WaitForSeconds(1f / AttackSpeed);
                while (target.Health > 0 && (target.transform.position - transform.position).sqrMagnitude < range)
                {
                    target.OnDamage(Damage);
                    yield return attackTimer;
                }
            }
        }
        public override void OnDeath()
        {
            StopAllCoroutines();
            currentTile.isOccupied = false;
            OnDiedEvent(null);
            Destroy(gameObject);
        }
    }
}