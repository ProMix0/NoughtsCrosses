namespace SeaBattle
{
    public class Ship
    {
        public Ship()
        {
            size = new(1, 1);
        }

        protected Ship(Ship ship)
        {
            size = ship.size;
        }

        public readonly Point size;

        public PinnedShip AsPinned(Point position)
        {
            return new(position, this);
        }
    }
}