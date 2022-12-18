using UnityEngine;
using UnityEngine.EventSystems;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayerMask;
        [SerializeField] private Camera cam;
        [SerializeField] private CameraController camController;
        private ISelectable currentInteractable;
        private Vector3 lastMousePosition;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (currentInteractable != null)
                {
                    currentInteractable.OnDeselected();
                    currentInteractable = null;
                }

                RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, interactableLayerMask);
                if (hit)
                {
                    currentInteractable = hit.transform.GetComponent<ISelectable>();
                    currentInteractable.OnSelected();
                }
            }
        }
    }
}