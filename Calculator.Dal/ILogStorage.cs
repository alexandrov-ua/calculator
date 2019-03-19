namespace Calculator.Dal
{
    public interface ILogStorage<T>
    {
        void Log(T obj);
        LogEntry<T>[] GetAll();
    }
}