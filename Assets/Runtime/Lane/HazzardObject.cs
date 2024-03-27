using Runtime.Interfaces;
using Runtime.Game;
using UnityEngine;

public class HazzardObject : MonoBehaviour, ILaneObject
{
    [SerializeField] private float minConsumeScale = 3f;
    [SerializeField] private GameObject slopeObject;
    [SerializeField] private GameObject snowballObject;

    private void OnEnable() 
    {
        slopeObject.SetActive(true);
        snowballObject.SetActive(false);
    }

    public void OnSlopeLaneHit(Snowball snowball)
    {
        if(snowball.Scale < minConsumeScale) snowball.OnHazzardCollision();
        else slopeObject.gameObject.SetActive(false); //TODO : This should put the object onto the snowball.
    }

    public void OnSnowballLaneHit(Snowball snowball)
    {
        snowball.OnHazzardCollision();
        snowballObject.gameObject.SetActive(false);
    }
}