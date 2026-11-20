using UnityEngine;
using StarterAssets;

public class Coin : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        ThirdPersonController controller =
            other.GetComponentInParent<ThirdPersonController>();

        if (controller == null)
            return;

        if (!other.CompareTag("Player") && !controller.CompareTag("Player"))
            return;

        collected = true;

        controller.MoveSpeed += 1f;
        controller.SprintSpeed += 1f;

        PlayerCollectibleCounts counts =
            controller.GetComponentInParent<PlayerCollectibleCounts>();

        if (counts != null)
        {
            counts.AddCoin();
        }
        else
        {
            Debug.LogWarning("Adicione PlayerCollectibleCounts ao robô.", controller);
        }

        Destroy(gameObject);
    }
}