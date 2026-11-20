using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1CoinText;
    [SerializeField] private TMP_Text player2CoinText;

    private void Awake()
    {
        player1CoinText.text = "Moedas: 0";
        player2CoinText.text = "Moedas: 0";
    }

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
        if (player == null) return;

        switch (player.name)
        {
            case "PlayerRobot":
                player1CoinText.text = $"Moedas: {amount}";
                break;

            case "PlayerRobot (1)":
                player2CoinText.text = $"Moedas: {amount}";
                break;
        }
    }
}