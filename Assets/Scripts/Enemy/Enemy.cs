using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float turn_speed;

    public bool playerDetected;

    private void Update()
    {
        rotateTowards(target.position);
    }

    protected void rotateTowards(Vector3 to)
    {

        Quaternion _lookRotation = Quaternion.LookRotation((to - transform.position).normalized);

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turn_speed);
    }

    void EnemyShoot()
    {

    }
}
