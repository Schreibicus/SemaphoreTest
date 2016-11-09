using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemaphoreTest.View;

namespace SemaphoreTest.Presenter
{
    public class PresenterSemaphore : IPresenterSemaphore
    {
        IViewSemaphore _view;

        public void ShowView() { _view.Show(); }
        public void HideView() { _view.Hide(); }
    }
}
