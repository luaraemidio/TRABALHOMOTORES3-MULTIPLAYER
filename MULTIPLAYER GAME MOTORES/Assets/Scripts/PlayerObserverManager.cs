using UnityEngine;
using System;

public class PlayerObserverManager : MonoBehaviour
{
    public static Action<GameObject> OnCoinCollected;
    public static Action<GameObject, int> OnCoinCountChanged;

    public static Action<GameObject> OnStarCollected;
    public static Action<GameObject, int> OnStarCountChanged;

    public static void NotifyCoinCollected(GameObject player)
    {
        OnCoinCollected?.Invoke(player);
    }

    public static void NotifyCoinCountChanged(GameObject player, int amount)
    {
        OnCoinCountChanged?.Invoke(player, amount);
    }

    public static void NotifyStarCollected(GameObject player)
    {
        OnStarCollected?.Invoke(player);
    }

    public static void NotifyStarCountChanged(GameObject player, int amount)
    {
        OnStarCountChanged?.Invoke(player, amount);
    }
}