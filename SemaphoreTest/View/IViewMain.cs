using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemaphoreTest.View
{
    public interface IViewMain
    {
        void Show();            //запустить основное окно приложэения
        void Close();           //закрыть основное окно приложэения
        event Action AppLoad;   //событие - загрузка основной формы


        //получение интерфейсных ссылок на дочерние Представления
        IViewSemaphore GetSemaphoreView();
    }
}
