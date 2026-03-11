using UnityEngine;

public class EnemyDetectionZone : MonoBehaviour
{
    public Enemy enemyScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enemyScript.playerDetected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemyScript.playerDetected = false;
    }
}
