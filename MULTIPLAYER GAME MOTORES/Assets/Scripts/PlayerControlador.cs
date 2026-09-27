using UnityEngine;

public class PlayerControlador : MonoBehaviour
{
    private int coinCount;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            PlayerOM.ChangeCoinCount(coinCount);
            Destroy(other.gameObject);
        }
    }
}