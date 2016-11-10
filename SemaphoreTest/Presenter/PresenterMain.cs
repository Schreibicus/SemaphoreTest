using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemaphoreTest.View;

namespace SemaphoreTest.Presenter
{
    public class PresenterMain : IPresenterMain
    {
        //Контроллируемое представление
        private readonly IViewMain _viewMain;

        //Дочерние презентеры
        private IPresenterSemaphore _presenterSemaphore;


        public PresenterMain(IViewMain viewMain)
        {
            _viewMain = viewMain;
            _viewMain.AppLoad += AppLoadHandler;
        }

        public void Run()
        {
            _viewMain.Show();
        }

        private void AppLoadHandler()
        {
            //запустить Презентер семафора
            InitSemaphoreScreen();
            ShowSemaphoreScreen();
        }

        private void ShowSemaphoreScreen()
        {
            _presenterSemaphore?.ShowView();
        }

        private void InitSemaphoreScreen()
        {
            if (_presenterSemaphore == null) {
                _presenterSemaphore = new PresenterSemaphore(_viewMain.GetSemaphoreView());
            }
        }
    }
}
