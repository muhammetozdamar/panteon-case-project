using System;
using UnityEngine;
using UnityEngine.UI;

namespace BaridaGames.PanteonCaseProject.Gameplay.UI
{
    public class BuildingUI : MonoBehaviour
    {
        [SerializeField] private BuildingBase building;
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        internal BuildingBase Building => building;
        private void Awake()
        {
            image.sprite = building.data.Icon;
        }
        internal void SetOnClick(Action onClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick());
        }
    }
}