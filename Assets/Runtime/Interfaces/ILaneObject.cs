using Runtime.Game;

namespace Runtime.Interfaces
{
    public interface ILaneObject
    {
        void OnSlopeLaneHit(Snowball snowball);
        void OnSnowballLaneHit(Snowball snowball);
    }
}