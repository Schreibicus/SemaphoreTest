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


        public MyTimedThread(Semaphore semaphore, string name)
        {
            _semaphore = semaphore;
            _thread = new Thread(MainThreadLoop) {Name = name};
            IsRunning = false;
            DisplayString = $"Thread {name} --> New";
        }

        public void Start()
        {
            _thread.Start();
            DisplayString = $"Thread {Name} --> Waiting";
        }

        private void MainThreadLoop()
        {
            while (IsRunning) {
                if (!_semaphore.WaitOne(1500)) continue;

                try {
                    DisplayString = $"Thread {Name} --> Running";
                    while (IsRunning) {
                        //add timer logic
                        Thread.Sleep(2000);
                    }              
                }
                finally {
                    _semaphore.Release();
                }
            }
        }



    }
}
