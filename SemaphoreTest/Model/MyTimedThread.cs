using System;
using System.Security.AccessControl;
using System.Threading;

namespace SemaphoreTest.Model
{
    public class MyTimedThread : BindableBase
    {
        private readonly Semaphore _semaphore;
        private readonly Thread _thread;
        
        private string _displayString;
        public string DisplayString
        {
            get { return _displayString; }
            set { SetProperty(ref _displayString, value); }
        }

        public string Name { get { return _thread.Name; } }
        public bool IsRunning { get; set; }
        public event Action<string> ThreadEnteredWorkingArea;
        public event Action<string> ThreadExitedWorkingArea;



        public MyTimedThread(Semaphore semaphore, string name)
        {
            _semaphore = semaphore;
            _thread = new Thread(ThreadWorkingArea) { Name = name, IsBackground = true };
            IsRunning = false;
            DisplayString = $"Thread {name} --> New";
        }

        public void Start()
        {
            _thread.Start();
            DisplayString = $"Thread {Name} --> Waiting";
            IsRunning = true;
        }


        private void ThreadWorkingArea()
        {
            try {
                _semaphore.WaitOne();
                ThreadEnteredWorkingArea?.Invoke(Name);

                int workingTime = 0;
                var timer = new Timer(neverUsed => workingTime++, null, 0, 1000);

                while (IsRunning) {
                    DisplayString = $"Thread {Name} --> Running {workingTime}";
                    Thread.Sleep(500);             
                }
                
            }
            finally {
                ThreadExitedWorkingArea?.Invoke(Name);
                _semaphore.Release();
            }                  
        }

        
    }
}
