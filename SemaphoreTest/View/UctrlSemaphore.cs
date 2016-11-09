using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemaphoreTest.View
{
    public partial class UctrlSemaphore : UserControl, IViewSemaphore
    {
        public UctrlSemaphore()
        {
            InitializeComponent();
        }
    }
}
