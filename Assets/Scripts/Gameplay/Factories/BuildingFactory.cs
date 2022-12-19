using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class BuildingFactory : MonoBehaviour
    {
        internal BuildingBase GetBuilding(BuildingBase building, Vector3 position)
        {
            return Instantiate(building, position, Quaternion.identity);
        }
    }
}