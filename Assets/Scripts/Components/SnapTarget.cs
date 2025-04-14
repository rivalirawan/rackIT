using UnityEngine;

public class SnapTarget : MonoBehaviour
{
    public string acceptedTag = "CPU";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(acceptedTag))
        {
            other.transform.position = transform.position;
            other.GetComponent<Draggable>().enabled = false; // kunci posisi
        }
    }
}
