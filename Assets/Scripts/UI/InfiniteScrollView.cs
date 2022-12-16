using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BaridaGames.PanteonCaseProject.Gameplay.UI
{
    public class InfiniteScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField, Range(0f, 0.5f)] private float threshold;

        private int childAmount;
        private Vector2 heightOffset;
        private int gridColumnAmount;

        private void Start()
        {
            childAmount = scrollRect.content.childCount;
            heightOffset = Vector2.up * (grid.cellSize.y + grid.spacing.y);
            gridColumnAmount = grid.constraintCount;
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void OnScrollValueChanged(Vector2 value)
        {
            if (scrollRect.verticalNormalizedPosition <= threshold)
            {
                for (int i = 0; i < gridColumnAmount; i++)
                {
                    Transform firstChild = scrollRect.content.GetChild(childAmount - 1);
                    firstChild.SetSiblingIndex(0);
                }
                scrollRect.content.anchoredPosition -= heightOffset;
            }
            else if (scrollRect.verticalNormalizedPosition >= (1 - threshold))
            {
                for (int i = 0; i < gridColumnAmount; i++)
                {
                    Transform firstChild = scrollRect.content.GetChild(0);
                    firstChild.SetSiblingIndex(childAmount);
                }
                scrollRect.content.anchoredPosition += heightOffset;
            }
        }
    }
}