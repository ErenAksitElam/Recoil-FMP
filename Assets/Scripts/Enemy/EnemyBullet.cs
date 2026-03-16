using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Shooting player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            player = other.gameObject.transform.parent.GetComponent<Shooting>();
            //Will have to be changed once the elite enemy is added, will just make that 1 into a variable and have a separate Elite Enemy Bullet
            player.TakeDamage(1);
            Destroy(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
