using Runtime.Game;

namespace Runtime.Level
{
    public class SnowPile : LaneObject
    {
        public override Snowball Snowball { get; set; }
        public override void OnHit(Snowball snowball)
        {
            snowball.OnSnowPileCollision();
            ReturnToPool();
        }
    }
}