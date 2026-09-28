using UnityEngine;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        Invoke("GoToMenu", 2f);
    }

    private void GoToMenu()
    {
        GameManager.Instance.LoadScene("MenuPrincipal");
    }
}