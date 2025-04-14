using UnityEngine;

public class AssemblyValidator : MonoBehaviour
{
    public GameObject[] requiredParts;

    public bool IsAssemblyComplete()
    {
        foreach (var part in requiredParts)
        {
            if (part.GetComponent<Draggable>() != null)
                return false;
        }
        return true;
    }
}
