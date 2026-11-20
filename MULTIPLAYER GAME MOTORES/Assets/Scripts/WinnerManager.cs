using TMPro;
using UnityEngine;

public class WinnerManager : MonoBehaviour
{
    public static WinnerManager Instance;

    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText;

    private int remainingStars;
    private bool startedCounting;
    private bool matchEnded;

    private void Awake()
    {
        Instance = this;
    }

    public void StarCollected()
    {
        if (matchEnded)
            return;

        // A GUI pode carregar antes da fase. Conte as estrelas
        // na primeira coleta, enquanto o Pickup ainda existe na cena.
        if (!startedCounting)
        {
            remainingStars = FindObjectsOfType<Pickup>().Length;
            startedCounting = true;
        }

        remainingStars--;
        Debug.Log("Restam " + remainingStars + " estrelas.");

        if (remainingStars > 0)
            return;

        GameObject player1 = GameObject.Find("PlayerRobot");
        GameObject player2 = GameObject.Find("PlayerRobot (1)");

        if (player1 == null || player2 == null ||
            !player1.TryGetComponent(out PlayerCollectibleCounts player1Counts) ||
            !player2.TryGetComponent(out PlayerCollectibleCounts player2Counts))
        {
            Debug.LogError(
                "Confira se PlayerRobot e PlayerRobot (1) estão ativos " +
                "e têm PlayerCollectibleCounts no objeto principal.", this);
            return;
        }

        if (player1Counts.Stars > player2Counts.Stars)
        {
            ShowWinner("PLAYER 1 WINS!");
        }
        else if (player2Counts.Stars > player1Counts.Stars)
        {
            ShowWinner("PLAYER 2 WINS!");
        }
        else
        {
            ShowWinner("EMPATE!");
        }
    }

    public void ShowWinner(string winner)
    {
        matchEnded = true;
        winnerText.text = winner;
        winnerPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}