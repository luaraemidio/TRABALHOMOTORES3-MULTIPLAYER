using UnityEngine;

public class PlayerCollectibleCounts : MonoBehaviour
{
    private int coins;
    private int stars;
    public int Stars => stars;

    public void AddCoin()
    {
        coins++;
        PlayerObserverManager.NotifyCoinCountChanged(gameObject, coins);
    }

    public void AddStar()
    {
        stars++;
        PlayerObserverManager.NotifyStarCountChanged(gameObject, stars);
    }
}