using UnityEngine;

public class DragManager : MonoBehaviour
{
    private Camera mainCamera;
    private Draggable currentlyDragging;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Draggable draggable = hit.collider.GetComponent<Draggable>();
                if (draggable != null)
                {
                    currentlyDragging = draggable;
                    draggable.StartDrag();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentlyDragging != null)
            {
                currentlyDragging.EndDrag();
                currentlyDragging = null;
            }
        }

        if (currentlyDragging != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                currentlyDragging.UpdateDragPosition(hit.point);
            }
        }
    }
}
