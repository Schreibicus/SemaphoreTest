using System;
using System.Windows.Forms;

namespace SemaphoreTest.View
{
    public partial class FormMain : Form, IViewMain
    {
        public event Action AppLoad;

        public FormMain()
        {
            InitializeComponent();

            //Hide all user controls
            uctrlSemaphore.Hide();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Action appLoad = AppLoad;
            appLoad?.Invoke();
        }

        //Redefine show to run application
        public new void Show()
        {
            Application.Run(this);
        }

        public IViewSemaphore GetSemaphoreView()
        {
            return uctrlSemaphore;
        }

        

    }
}
