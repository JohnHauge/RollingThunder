using Runtime.Snow;

namespace Runtime.Interfaces
{
    public interface ILaneObject
    {
        void OnSlopeLaneHit(Snowball snowball);
        void OnSnowballLaneHit(Snowball snowball);
    }
}