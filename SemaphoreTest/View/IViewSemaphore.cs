using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
