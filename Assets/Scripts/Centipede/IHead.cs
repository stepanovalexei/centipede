namespace Centipede
{
    public interface IHead : ICentipedePart
    {
        void MoveTowards(Direction direction);
    }
}