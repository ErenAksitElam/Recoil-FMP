using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Enemy enemy;
    private Shooting player;

    private void Start()
    {
        player = (Shooting)FindAnyObjectByType(typeof(Shooting));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitbox"))
        {
            enemy = other.gameObject.transform.parent.GetComponent<Enemy>();
            //Will have to be changed once the elite enemy is added, will just make that 1 into a variable and have a separate Elite Enemy Bullet
            enemy.TakeDamage(player.damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
