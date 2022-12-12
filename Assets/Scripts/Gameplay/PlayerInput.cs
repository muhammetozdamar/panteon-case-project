using UnityEngine;
using UnityEngine.EventSystems;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayerMask;
        [SerializeField] private Camera cam;
        [SerializeField] private CameraController camController;
        private IInteractable currentInteractable;
        private Vector3 lastMousePosition;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
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
                    currentInteractable = null;
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
                    Vector2 delta = lastMousePosition - Input.mousePosition;
                    camController.Move(delta.normalized);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (currentInteractable != null)
                {
                    currentInteractable.OnMouseUp();
                }
            }
            lastMousePosition = Input.mousePosition;
        }
    }
}