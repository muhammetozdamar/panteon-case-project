using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private LayerMask interactableLayerMask;
        private IInteractable currentInteractable;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, interactableLayerMask);
                if (hit)
                {
                    currentInteractable = hit.transform.GetComponent<IInteractable>();
                    currentInteractable.OnMouseDown();
                }
                else
                {
                    // Clicked on ground, reset UI
                    currentInteractable = null;
                    InformationPanelController.Instance.ResetPanel();
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (currentInteractable != null)
                {
                    currentInteractable.OnMouseHold();
                }
                else
                {
                    // Drag camera
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (currentInteractable != null)
                {
                    currentInteractable.OnMouseUp();
                }
            }
        }
    }
}