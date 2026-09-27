using UnityEngine;
using TMPro;

public class MoedaUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    

    private void Awake()
    {
        
        PlayerOM.OnCoinChanged -= UpdateCoinCount; 
    }

    private void OnEnable()
    {
        PlayerOM.OnCoinChanged += UpdateCoinCount;
    }

    private void OnDisable()
    {
        PlayerOM.OnCoinChanged-= UpdateCoinCount;
    }

    private void UpdateCoinCount(int moeda)
    {
        if (coinText != null)
            coinText.text = $"Moedas: {moeda}";
    }
}