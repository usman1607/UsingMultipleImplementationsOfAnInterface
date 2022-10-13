namespace UsingMultipleImplementationsOfAnInterface.Services
{
    public class DbLogger : ICustomLogger
    {
        public bool Write(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
