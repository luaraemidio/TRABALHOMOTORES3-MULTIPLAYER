using TMPro;
using UnityEngine;
using StarterAssets;

public class WinnerManager : MonoBehaviour
{
    public static WinnerManager Instance;

    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText;

    private int remainingStars;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pickup[] stars = FindObjectsOfType<Pickup>();

        remainingStars = stars.Length;

        Debug.Log("Total de estrelas: " + remainingStars);
    }

    public void StarCollected()
    {
        remainingStars--;

        Debug.Log("Restam " + remainingStars + " estrelas.");

        if (remainingStars <= 0)
        {
            ThirdPersonController[] players = FindObjectsOfType<ThirdPersonController>();

            int player1 = 0;
            int player2 = 0;

            foreach (ThirdPersonController player in players)
            {
                if (player.transform.parent.name.StartsWith("PlayerRobot (1)"))
                {
                    player2 = player.CoinCount;
                }
                else
                {
                    player1 = player.CoinCount;
                }
            }

            if (player1 > player2)
            {
                ShowWinner("PLAYER 1 WINS!");
            }
            else if (player2 > player1)
            {
                ShowWinner("PLAYER 2 WINS!");
            }
            else
            {
                ShowWinner("EMPATE!");
            }
        }
    }

    public void ShowWinner(string winner)
    {
        winnerPanel.SetActive(true);
        winnerText.text = winner;
        Time.timeScale = 0f;
    }
}