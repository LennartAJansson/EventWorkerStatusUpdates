namespace WorkerService1
{
    public class MyDataClass : IDisposable
    {
        public event MyEventType? MyStatusChanged;
        public int MyStatus { get => myStatus; set { myStatus = value; MyStatusChanged?.Invoke(myStatus); } }
        private int myStatus;

        public event MyEventType? MyStatus2Changed;
        public int MyStatus2 { get => myStatus2; set { myStatus2 = value; MyStatus2Changed?.Invoke(myStatus2); } }
        private int myStatus2;
        private readonly ILogger<MyDataClass> logger;

        public MyDataClass(ILogger<MyDataClass> logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            logger?.LogWarning("Disposing MyDataClass");
        }
    }

    public delegate void MyEventType(int status);
}
