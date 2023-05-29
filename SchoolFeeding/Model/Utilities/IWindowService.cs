using SchoolFeeding.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolFeeding.Model.Utilities
{
    public interface IWindowService
    {
        void OpenWindow(Window ExecWindow, bool CloseToken);
        void CloseWindow();
    }
}
