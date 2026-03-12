using UnityEngine;

public class MouseLock : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
