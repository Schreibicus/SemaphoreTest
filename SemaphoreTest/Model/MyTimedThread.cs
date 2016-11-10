﻿using System;
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
        public event Action<string> ThreadUnlocked;


        public MyTimedThread(Semaphore semaphore, string name)
        {
            _semaphore = semaphore;
            _thread = new Thread(MainThreadLoop)
            {
                Name = name,
                IsBackground = true
            };
            IsRunning = false;
            DisplayString = $"Thread {name} --> New";
        }

        public void Start()
        {
            _thread.Start();
            DisplayString = $"Thread {Name} --> Waiting";
        }

        public void Abort()
        {
            _semaphore.Release();
            _thread.Abort();
        }

        private void MainThreadLoop()
        {
            bool stop = false;

            while (!stop) {
                if (!_semaphore.WaitOne(1500)) continue;

                try {
                    ThreadUnlocked?.Invoke(Name);
                    DisplayString = $"Thread {Name} --> Running";
                    while (IsRunning) {
                        //add timer logic
                        Thread.Sleep(2000);
                    }              
                }
                finally {
                    stop = true;
                    _semaphore.Release();
                }
            }
        }
    }
}
