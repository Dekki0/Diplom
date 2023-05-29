using SchoolFeeding.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolFeeding.Model.Utilities
{
    public class WindowService : IWindowService
    {
        public void OpenWindow(Window ExecWindow, bool CloseToken)
        {
            ExecWindow.Show();
            if (CloseToken)
                foreach (Window CloseWindow in Application.Current.Windows)
                {
                    if (CloseWindow != ExecWindow)
                        CloseWindow?.Close();
                }
        }
        public void CloseWindow()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window?.Close();
        }
    }
}
