using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.CollectCoin(); // Aumenta velocidade do robô
            Destroy(gameObject);  // Destrói a moeda
        }
    }
}