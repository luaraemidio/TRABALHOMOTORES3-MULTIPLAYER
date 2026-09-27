using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Nomes das Cenas")]
    [SerializeField] private string gameplaySceneName = "Gameplay";
    [SerializeField] private string guiSceneName = "GUI";

    [Header("Condição de Vitória")]
    [SerializeField] private int totalStarsInMap = 10;

    private int[] playerStars = new int[2];
    private int totalCollectedStars = 0;

    // Eventos (Padrão Observer)
    public static event Action<int, int> OnStarCollected; // (playerIndex, starCount)
    public static event Action<string> OnGameOver;        // (winnerMessage)

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(LoadGameScenes());
    }

    private IEnumerator LoadGameScenes()
    {
        // Carrega Gameplay
        AsyncOperation gameplayLoad = SceneManager.LoadSceneAsync(gameplaySceneName, LoadSceneMode.Single);
        while (!gameplayLoad.isDone) yield return null;

        // Carrega GUI em modo Aditivo
        AsyncOperation guiLoad = SceneManager.LoadSceneAsync(guiSceneName, LoadSceneMode.Additive);
        while (!guiLoad.isDone) yield return null;

        ResetGameState();
    }

    public void AddStarToPlayer(int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= playerStars.Length) return;

        playerStars[playerIndex]++;
        totalCollectedStars++;

        // Notifica inscritos (Observer)
        OnStarCollected?.Invoke(playerIndex, playerStars[playerIndex]);

        if (totalCollectedStars >= totalStarsInMap)
        {
            DetermineWinner();
        }
    }

    private void DetermineWinner()
    {
        string winnerMessage;

        if (playerStars[0] > playerStars[1])
        {
            winnerMessage = "JOGADOR 1 VENCEU! (Coletou mais estrelas)";
        }
        else if (playerStars[1] > playerStars[0])
        {
            winnerMessage = "JOGADOR 2 VENCEU! (Coletou mais estrelas)";
        }
        else
        {
            winnerMessage = "EMPATE!";
        }

        OnGameOver?.Invoke(winnerMessage);
        Time.timeScale = 0f; // Pausa o jogo ao terminar
    }

    private void ResetGameState()
    {
        Time.timeScale = 1f;
        playerStars[0] = 0;
        playerStars[1] = 0;
        totalCollectedStars = 0;
    }
}