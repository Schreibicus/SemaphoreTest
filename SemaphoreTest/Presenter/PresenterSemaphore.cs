using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SemaphoreTest.Model;
using SemaphoreTest.View;

namespace SemaphoreTest.Presenter
{
    public class PresenterSemaphore : IPresenterSemaphore
    {
        private readonly IViewSemaphore _view;

        private readonly Semaphore _semaphore;

        private readonly BindingList<MyTimedThread> _newThreads;
        private readonly BindingList<MyTimedThread> _waitingThreads;
        private readonly BindingList<MyTimedThread> _workingThreads;

        private int _newThreadNumber = 1;


        public PresenterSemaphore(IViewSemaphore view)
        {
            _semaphore = new Semaphore(1, 100, "MainSemaphore");

            _newThreads = new BindingList<MyTimedThread>();
            _waitingThreads = new BindingList<MyTimedThread>();
            _workingThreads = new BindingList<MyTimedThread>();

            _view = view;
            _view.SetNewThreadsDataSource(_newThreads);
            _view.SetWaitingThreadsDataSource(_waitingThreads);
            _view.SetWorkingThreadsDataSource(_workingThreads);

            _view.AddThreadRequested += AddThread;
            _view.ThreadStartRequested += StartThread;
            _view.ThreadStopRequested += StopThread;
            _view.SemaphoreCapacityRequested += ChangeSemaphoreCapcity;
        }


        private void AddThread()
        {
            MyTimedThread newThread = new MyTimedThread(_semaphore, _newThreadNumber.ToString());
            newThread.ThreadEnteredWorkingArea += OnThreadEnteredWorkingArea;

            newThread.ThreadExitedWorkingArea += OnThreadExitedWorkingArea;

            _newThreads.Add(newThread);
            _newThreadNumber++;
        }

        
        private void StartThread(string name)
        {
            var selectedThread = _newThreads.FirstOrDefault(thrd => thrd.Name == name);
            if (selectedThread == null) { return; }

            _newThreads.Remove(selectedThread);
            _waitingThreads.Add(selectedThread);
            selectedThread.IsRunning = true;
            selectedThread.Start();
        }


        private void OnThreadEnteredWorkingArea(string name)
        {
            var selectedThread = _waitingThreads.FirstOrDefault(thrd => thrd.Name == name);
            if (selectedThread == null) { return; }

            _workingThreads.Add(selectedThread);
            _waitingThreads.Remove(selectedThread);
            
        }

        private void OnThreadExitedWorkingArea(string name)
        {
            var selectedThread = _workingThreads.FirstOrDefault(thrd => thrd.Name == name);
            if (selectedThread == null) { return; }
            _workingThreads.Remove(selectedThread);
        }


        private void StopThread(string name)
        {
            var selectedThread = _workingThreads.FirstOrDefault(thrd => thrd.Name == name);
            if (selectedThread == null) { return; }
            selectedThread.IsRunning = false;
        }


        private void ChangeSemaphoreCapcity(int requestedCapacity)
        {
            
        }

        public void ShowView() { _view.Show(); }
        public void HideView() { _view.Hide(); }
    }
}
