using System;
using System.ComponentModel;
using SemaphoreTest.Model;

namespace SemaphoreTest.View
{
    public interface IViewSemaphore
    {
        void Show();
        void Hide();

        event Action AddThreadRequested;
        event Action<int> SemaphoreCapacityRequested;
        event Action<string> ThreadStartRequested;
        event Action<string> ThreadStopRequested;
        
        void SetNewThreadsDataSource(BindingList<MyTimedThread> dataSource);
        void SetWaitingThreadsDataSource(BindingList<MyTimedThread> dataSource);
        void SetWorkingThreadsDataSource(BindingList<MyTimedThread> dataSource);

        void SetThreadCounterControlBounds(int min, int max);
        void SetThreadCounterControlValue(int value);

        void ShowWarning(string warningMessage);
    }
}
