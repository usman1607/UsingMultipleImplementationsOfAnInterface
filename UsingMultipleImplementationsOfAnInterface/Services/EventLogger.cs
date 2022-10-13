namespace UsingMultipleImplementationsOfAnInterface.Services
{
    public class EventLogger : ICustomLogger
    {
        public bool Write(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
