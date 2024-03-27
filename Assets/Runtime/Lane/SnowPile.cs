using Runtime.Interfaces;
using Runtime.Game;
using UnityEngine;

public class SnowPile : MonoBehaviour, ILaneObject
{
    public void OnSlopeLaneHit(Snowball snowball)
    {
        snowball.OnSnowPileCollision();
        gameObject.SetActive(false);
    }

    public void OnSnowballLaneHit(Snowball snowball) {}
}