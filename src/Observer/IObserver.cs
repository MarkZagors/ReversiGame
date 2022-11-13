namespace ReversiGame
{
    public interface IObserver
    {
        public void Update(Globals.Signal signal);
    }
}