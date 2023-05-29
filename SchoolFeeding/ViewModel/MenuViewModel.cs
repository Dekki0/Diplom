using SchoolFeeding.Model.Utilities;
using SchoolFeeding.View;
using SchoolFeeding.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolFeeding.ViewModel
{
    public class MenuViewModel
    {
        #region private varible
        private readonly IWindowService _windowService;
        #endregion

        #region public property
        public ICommand[] MenuCommand { get; }
        public bool IsButtonVisible { get; set; } = false;
        #endregion

        #region constructor
        public MenuViewModel() 
        {
            _windowService = new WindowService();
            MenuCommand = new RelayCommand<object>[3]
            {
              new RelayCommand<object>(Logout),
              new RelayCommand<object>(Logout),
              new RelayCommand<object>(Logout)
            };
            if (Configure.GetParametr("CurrentRole").Equals("Админ"))
                IsButtonVisible = true;
        }
        #endregion

        #region private methods
        private void Logout(object args)
        {
            if (MessageBox.Show("Вы точно хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                _windowService.OpenWindow(new LoginView(), true);
        }
        #endregion
    }
}
