namespace ReversiGame
{
    public interface ISubject
    {
        public void Attach(IObserver observer);
        public void Detach(IObserver observer);
        public void Emit(Globals.Signal signal);
    }
}