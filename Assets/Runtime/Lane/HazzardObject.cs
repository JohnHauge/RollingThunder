using Runtime.Interfaces;
using Runtime.Game;
using UnityEngine;

public class HazzardObject : MonoBehaviour, ILaneObject
{
    [SerializeField] private float minConsumeScale = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject slopeObject;
    [SerializeField] private GameObject snowballObject;

    private void OnEnable()
    {
        slopeObject.SetActive(true);
        snowballObject.SetActive(false);
    }

    private void OnDisable()
    {
        slopeObject.SetActive(false);
        snowballObject.SetActive(false);
    }

    public void OnSlopeLaneHit(Snowball snowball)
    {
        if (snowball.transform.localScale.x < minConsumeScale) snowball.OnHazzardCollision(damage);
        // else -> TODO : This should put the object onto the snowball.
        enabled = false;
    }

    public void OnSnowballLaneHit(Snowball snowball)
    {

        //snowball.OnHazzardCollision();
        snowballObject.gameObject.SetActive(false);
        enabled = false;
    }
}