using Microsoft.IdentityModel.Tokens;
using SchoolFeeding.Model.DatabaseModel;
using SchoolFeeding.Model.Utilities;
using SchoolFeeding.View;
using SchoolFeeding.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolFeeding.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region private varible
        private readonly SchoolFeedingContext _Context;
        private readonly IWindowService _windowService;
        private string _login;
        private string _password;
        #endregion

        #region public property
        public string Login
		{
			get =>_login;
			set => SetProperty(ref _login, value.Trim());
		}
		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}
		public ICommand LoginCommand { get;}
        #endregion

        #region public constructor
        public LoginViewModel()
		{
            _Context = new SchoolFeedingContext();
			LoginCommand = new RelayCommand<object>(ChangePage);
            _windowService = new WindowService();
        }
        #endregion

        #region private methods
        private void ChangePage(object args)
		{
			if(Password.IsNullOrEmpty()||Login.IsNullOrEmpty())
			{
				MessageBox.Show("Введите логин и пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (_Context.Users.Any(user => user.Login.Equals(Login) && user.Password.Equals(Password)))
			{
				var User = _Context.Users.First(user => user.Login.Equals(Login) && user.Password.Equals(Password));
                Configure.SetParametr("CurrentRole", User.Role);
                Configure.CurrentUser = User;
                Console.WriteLine($"User: {User.Login}\nRole: {User.Role}");
				MessageBox.Show("Авторизация успешна","Succes",MessageBoxButton.OK,MessageBoxImage.Asterisk);
                _windowService.OpenWindow(new MainWindow(), true);
            }
			else
                MessageBox.Show("Неверный логин или пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

    }
}
