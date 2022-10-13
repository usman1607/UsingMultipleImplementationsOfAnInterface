namespace UsingMultipleImplementationsOfAnInterface.Services
{
    public class FileLogger : ICustomLogger
    {
        public bool Write(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
