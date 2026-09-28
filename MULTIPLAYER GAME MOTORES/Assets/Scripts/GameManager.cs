using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    
    public PlayerInput playerInput;   
    
    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Iniciando);
        SceneManager.LoadScene("Splash");
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Estado atual: " + currentState);
    }

    public void LoadScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Splash":
                SceneManager.LoadScene(sceneName);
                break;

            case "MenuPrincipal":
                SceneManager.LoadScene(sceneName);
                ChangeState(GameState.MenuPrincipal);
                break;

            case "GetStarted_Scene":
                SceneManager.LoadScene(sceneName);

                SceneManager.LoadScene(
                    "GUI",
                    LoadSceneMode.Additive
                );
                
                ChangeState(GameState.Gameplay);
                break;

            default:
                Debug.LogWarning("Cena não reconhecida: " + sceneName);
                break;
        }
    }

    public void LoadMenu()
    {
        LoadScene("MenuPrincipal");
    }
    
    public void AssignPlayerInput(PlayerInput input)
    {
        playerInput = input;
        Debug.Log("PlayerInput alocado.");
    }
}