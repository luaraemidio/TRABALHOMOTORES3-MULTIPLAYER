using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            GameManager.Instance.AddStarToPlayer(player.PlayerIndex); // Soma estrela no GameManager
            Destroy(gameObject);                                      // Destrói a estrela
        }
    }
}