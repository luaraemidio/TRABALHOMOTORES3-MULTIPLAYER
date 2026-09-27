using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Mostradores de Moedas")]
    [SerializeField] private TextMeshProUGUI p1CoinsText;
    [SerializeField] private TextMeshProUGUI p2CoinsText;

    [Header("Mostradores de Estrelas")]
    [SerializeField] private TextMeshProUGUI p1StarsText;
    [SerializeField] private TextMeshProUGUI p2StarsText;

    [Header("Painel do Vencedor")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI winnerText;

    private void OnEnable()
    {
        
        PlayerController.OnCoinCollected += UpdateCoinUI;
        GameManager.OnStarCollected += UpdateStarUI;
        GameManager.OnGameOver += DisplayGameOver;
    }

    private void OnDisable()
    {
        
        PlayerController.OnCoinCollected -= UpdateCoinUI;
        GameManager.OnStarCollected -= UpdateStarUI;
        GameManager.OnGameOver -= DisplayGameOver;
    }

    private void Start()
    {
        UpdateCoinUI(0, 0);
        UpdateCoinUI(1, 0);
        UpdateStarUI(0, 0);
        UpdateStarUI(1, 0);

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    private void UpdateCoinUI(int playerIndex, int coinsCount)
    {
        if (playerIndex == 0 && p1CoinsText != null)
            p1CoinsText.text = $"P1 Moedas: {coinsCount}";
        else if (playerIndex == 1 && p2CoinsText != null)
            p2CoinsText.text = $"P2 Moedas: {coinsCount}";
    }

    private void UpdateStarUI(int playerIndex, int starsCount)
    {
        if (playerIndex == 0 && p1StarsText != null)
            p1StarsText.text = $"P1 Estrelas: {starsCount}";
        else if (playerIndex == 1 && p2StarsText != null)
            p2StarsText.text = $"P2 Estrelas: {starsCount}";
    }

    private void DisplayGameOver(string resultMessage)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (winnerText != null)
            {
                winnerText.text = resultMessage;
            }
        }
    }
}