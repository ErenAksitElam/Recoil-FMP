using UnityEngine;

public class CombatAreaDetection : MonoBehaviour
{
    public WaveManager waveManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            waveManager.playerEntered = true;
    }
}
