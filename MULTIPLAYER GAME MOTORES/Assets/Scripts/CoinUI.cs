
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text coinText2;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinCountChanged += UpdateCoins;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCountChanged -= UpdateCoins;
    }

    private void UpdateCoins(GameObject player, int amount)
    {
        if (player == player1)
        {
            coinText.text = "Jogador 1: " + amount;
        }
        else if (player == player2)
        {
            coinText2.text = "Jogador 2: " + amount;
        }
    }
}
