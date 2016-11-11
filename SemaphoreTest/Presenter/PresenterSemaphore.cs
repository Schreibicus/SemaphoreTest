using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
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
        private readonly BindingList<MyTimedThread> _placeholderThreads;


        private int _newThreadNumber = 1;
        private const int SemaphoreMaxCapacity = 100;
        private const int SemaphoreMinCapacity = 1;
        private int _semaphoreCurrentCapacity = 1;


        public PresenterSemaphore(IViewSemaphore view)
        {
            _semaphore = new Semaphore(_semaphoreCurrentCapacity, SemaphoreMaxCapacity);
            
            _newThreads = new BindingList<MyTimedThread>();
            _waitingThreads = new BindingList<MyTimedThread>();
            _workingThreads = new BindingList<MyTimedThread>();
            _placeholderThreads = new BindingList<MyTimedThread>();

            _view = view;
            _view.SetNewThreadsDataSource(_newThreads);
            _view.SetWaitingThreadsDataSource(_waitingThreads);
            _view.SetWorkingThreadsDataSource(_workingThreads);
            _view.SetThreadCounterControlBounds(SemaphoreMinCapacity, SemaphoreMaxCapacity);
            _view.SetThreadCounterControlValue(_semaphoreCurrentCapacity);

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
                    ReleaseSemaphoreCount();
                }

                return;
            }

            //Capacity decrease requested - may need to stop thread
            for (int i = 0; i < delta; i++) {
                if (requestedCapacity < _workingThreads.Count) {
                    RetireOldestThread();

                }
                else {
                    GrabSemaphoreCount();
                }
            }

        }


        /// <summary>
        /// Releases one semaphore count
        ///  </summary>
        /// <remarks>
        /// When a placeholder thread is present - stops it to release count
        /// Otherwise releases count from the semaphore's maximum count reserve
        /// </remarks>
        private void ReleaseSemaphoreCount()
        {
            if (_placeholderThreads.Count < 1) {
                _semaphore.Release();
                _semaphoreCurrentCapacity++;
                return;
            }

            var placeholder = _placeholderThreads[0];
            placeholder.IsRunning = false;
            placeholder.Join();
            _placeholderThreads.Remove(placeholder);
           _semaphoreCurrentCapacity++;
        }


        /// <summary>
        /// Grabs one semahore count with a placeholder thread
        /// </summary>
        private void GrabSemaphoreCount()
        {
            var placeholder = new MyTimedThread(_semaphore, Guid.NewGuid().ToString());
            _placeholderThreads.Add(placeholder);
            placeholder.Start();
            _semaphoreCurrentCapacity--;
        }


        /// <summary>
        /// Removes MyThread from working list
        /// and grabs one semahore count with it as a placeholder
        /// </summary>
        private void RetireOldestThread()
        {
            var oldestThread = _workingThreads.Max();
            if (oldestThread == null) { return; }
            _placeholderThreads.Add(oldestThread);
            _workingThreads.Remove(oldestThread);
            _semaphoreCurrentCapacity--;
        }


        public void ShowView() { _view.Show(); }
        public void HideView() { _view.Hide(); }
    }
}
