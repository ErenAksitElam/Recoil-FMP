using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public RankingMenu ranking;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ranking.playerFinished = true;
        }

    }
}
