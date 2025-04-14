using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AssemblyValidator validator;

    public void CheckAssembly()
    {
        if (validator.IsAssemblyComplete())
        {
            Debug.Log("Perakitan selesai!");
        }
        else
        {
            Debug.Log("Masih ada part yang belum dipasang.");
        }
    }
}
