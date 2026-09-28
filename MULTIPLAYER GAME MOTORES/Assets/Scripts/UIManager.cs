using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    
    private void OnEnable()
    {
        PlayerObserverManager.OnCoinCountChanged += UpdateScore;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCountChanged -= UpdateScore;
    }

    private void Start()
    {
        player1Score.text = "PLAYER 1 SCORE: 0";
        player2Score.text = "PLAYER 2 SCORE: 0";
    }
    
    private void UpdateScore(GameObject player, int score)
    {
        if (player.name.StartsWith("PlayerRobot (1)"))
        {
            player2Score.text = "PLAYER 2 SCORE: " + score;
        }
        else
        {
            player1Score.text = "PLAYER 1 SCORE: " + score;
        }
    }
}