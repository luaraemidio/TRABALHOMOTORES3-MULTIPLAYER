using UnityEngine;
using System;

public static class PlayerOM
{
    public static event Action OnCoinCollected;
    public static event Action<int> OnCoinChanged;

    public static void CollectCoin()
    {
        OnCoinCollected?.Invoke();
    }
    public static void ChangeCoinCount(int coinCount)
    {
        OnCoinChanged?.Invoke(coinCount);
    }

    
    public static void ClearListeners()
    {
        OnCoinCollected = null;
    }
}