using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;

    public void StartDrag()
    {
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
    }

    public void UpdateDragPosition(Vector3 position)
    {
        if (isDragging)
        {
            transform.position = new Vector3(position.x, position.y + 0.3f, position.z); // geser ke atas biar keliatan
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SnapTarget target = other.GetComponent<SnapTarget>();
        if (target != null && target.acceptedTag == tag)
        {
            transform.position = target.transform.position;
            transform.rotation = target.transform.rotation;
        }
    }
}
