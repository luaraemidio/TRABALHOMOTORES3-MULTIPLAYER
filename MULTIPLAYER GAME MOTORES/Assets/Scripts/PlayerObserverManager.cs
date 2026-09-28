using UnityEngine;
using System;

public class PlayerObserverManager : MonoBehaviour
{
    public static Action<GameObject, int> OnCoinCountChanged;
    
    public static Action<GameObject> OnCoinCollected;

    public static void NotifyCoinCollected(GameObject player)
    {
        OnCoinCollected?.Invoke(player);
    }

    public static void NotifyCoinCountChanged(GameObject player, int amount)
    {
        OnCoinCountChanged?.Invoke(player, amount);
    }
}