using System;
using System.ComponentModel;
using System.Windows.Forms;
using SemaphoreTest.Model;

namespace SemaphoreTest.View
{
    public partial class UctrlSemaphore : UserControl, IViewSemaphore
    {
        public event Action AddThreadRequested;
        public event Action<int> SemaphoreCapacityRequested;
        public event Action<string> ThreadStartRequested;
        public event Action<string> ThreadStopRequested;

        private readonly BindingSource _bsNewThreads;
        private readonly BindingSource _bsWaitingThreads;
        private readonly BindingSource _bsWorkingThreads;


        public UctrlSemaphore()
        {
            InitializeComponent();

            btnAddThread.Click += (sender, args) => AddThreadRequested?.Invoke();
            numSemaphoreCapacity.ValueChanged +=
                (sender, args) => SemaphoreCapacityRequested?.Invoke((int) numSemaphoreCapacity.Value);
            lbNewThreads.DoubleClick +=
                (sender, args) => ThreadStartRequested?.Invoke(lbNewThreads.SelectedValue as string);
            lbWorkingThreads.DoubleClick +=
                (sender, args) => ThreadStopRequested?.Invoke(lbWorkingThreads.SelectedValue as string);

            _bsNewThreads = new BindingSource();
            _bsWaitingThreads = new BindingSource();
            _bsWorkingThreads= new BindingSource();
        }

        
        public void SetNewThreadsDataSource(BindingList<MyTimedThread> dataSource)
        {
            _bsNewThreads.DataSource = dataSource;
            lbNewThreads.DisplayMember = "DisplayString";
            lbNewThreads.ValueMember = "Name";
            lbNewThreads.DataSource = _bsNewThreads;
        }


        public void SetWaitingThreadsDataSource(BindingList<MyTimedThread> dataSource)
        {
            _bsWaitingThreads.DataSource = dataSource;
            lbWaitingThreads.DisplayMember = "DisplayString";
            lbWaitingThreads.ValueMember = "Name";
            lbWaitingThreads.DataSource = _bsWaitingThreads;
        }


        public void SetWorkingThreadsDataSource(BindingList<MyTimedThread> dataSource)
        {
            _bsWorkingThreads.DataSource = dataSource;
            lbWorkingThreads.DisplayMember = "DisplayString";
            lbWorkingThreads.ValueMember = "Name";
            lbWorkingThreads.DataSource = _bsWorkingThreads;
        }


        public void SetThreadCounterControlBounds(int min, int max)
        {
            numSemaphoreCapacity.Minimum = min;
            numSemaphoreCapacity.Maximum = max;
        }


        public void SetThreadCounterControlValue(int value)
        {
            numSemaphoreCapacity.Value = value;
        }


        public void ShowWarning(string warningMessage)
        {
            MessageBox.Show(warningMessage, @"Warning", MessageBoxButtons.OK);
        }
    }
}
