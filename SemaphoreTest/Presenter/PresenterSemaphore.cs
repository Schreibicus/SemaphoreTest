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
        private const int SemaphoreMaxCapacity = 100;
        private const int SemaphoreMinCapacity = 1;
        private int _semaphoreCurrentCapacity = 1;


        public PresenterSemaphore(IViewSemaphore view)
        {
            _semaphore = new Semaphore(_semaphoreCurrentCapacity, SemaphoreMaxCapacity);
            //_view.SetThreadCounterControlBounds(SemaphoreMinCapacity, SemaphoreMaxCapacity);
            //_view.SetThreadCounterControlValue(_semaphoreCurrentCapacity);

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
            selectedThread.Join();
        }


        private void ChangeSemaphoreCapcity(int requestedCapacity)
        {
            //Capacity above maximum requested - reject
            if (requestedCapacity > SemaphoreMaxCapacity) {
                _view.ShowWarning($"Can't exceed Semaphore maximum capacity of {SemaphoreMaxCapacity}!");
                _view.SetThreadCounterControlValue(SemaphoreMaxCapacity);
                return;
            }

            //Capacity below minimum requested - reject
            if (requestedCapacity < SemaphoreMinCapacity) {
                _view.ShowWarning($"Semaphore capacity can't be lower than 1!");
                _view.SetThreadCounterControlValue(SemaphoreMinCapacity);
                return;
            }

            int delta = Math.Abs(requestedCapacity - _semaphoreCurrentCapacity);

            //Capacity increase requested - safe to grant
            if (requestedCapacity > _semaphoreCurrentCapacity) {
                for (int i = 0; i < delta; i++) {
                    IncrementSemaphoreCapacity();
                }
                return;
            }

            //Capacity decrease requested - may need to stop thread
            for (int i = 0; i < delta; i++) {
                if (requestedCapacity < _workingThreads.Count) {
                    StopOldestThread();
                }
                DecrementSemaphoreCapacity();
            }
        }

        private void IncrementSemaphoreCapacity()
        {
            _semaphore.Release();
            _semaphoreCurrentCapacity++;
        }

        private void DecrementSemaphoreCapacity()
        {
            _semaphore.WaitOne();
            _semaphoreCurrentCapacity--;
        }


        private void StopOldestThread()
        {
            var oldestThread = _workingThreads.Max();
            if (oldestThread == null) { return; }
            oldestThread.IsRunning = false;
            oldestThread.Join();
        }


        public void ShowView() { _view.Show(); }
        public void HideView() { _view.Hide(); }
    }
}
