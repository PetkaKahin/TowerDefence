namespace Towers
{
    public interface IMover
    {
        float Speed { get; }

        void Move();
        void SetSpeed(float speed);
    }
}