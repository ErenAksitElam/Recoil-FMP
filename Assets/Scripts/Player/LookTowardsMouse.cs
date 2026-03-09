using UnityEngine;

public class LookTowardsMouse : MonoBehaviour
{
    private Vector3 mouse_pos;
    private Transform target;
    private Vector3 object_pos;
    private float angle;

    private void Start()
    {
        target = gameObject.transform;
    }
    void Update()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f;
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
    }
}
