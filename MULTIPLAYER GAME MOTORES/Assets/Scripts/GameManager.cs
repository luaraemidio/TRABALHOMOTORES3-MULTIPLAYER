using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool scenesLoaded = false;

    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!scenesLoaded)
        {
            scenesLoaded = true;
            LoadAdditiveScenes();
        }
        PlayerOM.ClearListeners();
    }

    private void LoadAdditiveScenes()
    {
        
        if (SceneManager.GetSceneByName("GetStarted_Scene").isLoaded == false)
        {
            SceneManager.LoadSceneAsync("GetStarted_Scene", LoadSceneMode.Additive);
        }

        
        if (SceneManager.GetSceneByName("GUI").isLoaded == false)
        {
            SceneManager.LoadSceneAsync("GUI", LoadSceneMode.Additive);
        }
    }
}