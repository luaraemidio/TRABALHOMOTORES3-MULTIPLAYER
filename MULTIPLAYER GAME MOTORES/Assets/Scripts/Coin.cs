using UnityEngine;
using StarterAssets;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        ThirdPersonController controller = other.GetComponent<ThirdPersonController>();

        if (controller != null)
        {
            controller.MoveSpeed += 1f;
            controller.SprintSpeed += 1f;
        }

        Destroy(gameObject);
    }
}