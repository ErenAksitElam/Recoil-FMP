using UnityEngine;

public class Hole : MonoBehaviour
{
    private Shooting player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Shooting>();
            player.TakeDamage(500);
        }
    }
}
