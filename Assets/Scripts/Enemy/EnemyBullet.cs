using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Shooting player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            player = other.gameObject.transform.parent.GetComponent<Shooting>();
            player.TakeDamage(1);
        }
    }
}
