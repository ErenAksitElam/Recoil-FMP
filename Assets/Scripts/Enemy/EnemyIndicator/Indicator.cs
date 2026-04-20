     using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject indicator;
    private GameObject indicatorInst;
    private GameObject target;

    //private Transform canvasTransform;

    Renderer rd;

    private Quaternion lookRotation;
    public float turn_speed;

    private void Awake()
    {
        rd = GetComponent<Renderer>();
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        if (rd.isVisible == false)
        {
            if (indicator.activeSelf == false)
            {
                indicator.SetActive(true);
            }

            RaycastHit ray;
            Vector3 direction = target.transform.position - transform.position;

            Physics.Raycast(transform.position, direction, out ray);

            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
                lookRotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);

                indicator.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turn_speed);
                indicator.transform.Rotate(-90, 90, 0);
            }
        }
        else
        {
            if (indicator.activeSelf == true)
                indicator.SetActive(false);
        }
    }

    //empty obe3ert for each enemy otutside distance instantiate an arrow that looks ttowards that (list) make prefab that empty object inside that as a arrow and rotate accordingly
}
