using Runtime.Game;

namespace Runtime.Level
{
    public class SnowPile : LaneObject
    {
        public override void OnHit(Snowball snowball)
        {
            snowball.OnSnowPileCollision();
            gameObject.SetActive(false);
        }
    }
}