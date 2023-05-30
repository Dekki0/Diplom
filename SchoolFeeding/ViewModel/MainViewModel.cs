using SchoolFeeding.Model.Entities;
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
    public class MainViewModel : ViewModelBase
    {
        #region private varible

        private object? page;
        
        
        #endregion

        #region public block
        public object Page 
        {
            get => page??new MenuViewModel();
            private set => SetProperty(ref page, value, nameof(Page)); 
        }
        public ICommand BackPageCommand { get; }
        public MainViewModel()
        {
            StackControl.DataChanged += HandleDataChanged;
            StackControl.AddPage(new MenuViewModel());
            BackPageCommand = new RelayCommand<object>(BackPage);
        }

        #endregion

        #region private methods
        private void BackPage(object args) => Page=StackControl.GetPage()??new MenuViewModel();
        private void HandleDataChanged(object newData) => Page = newData;
        #endregion 
    }
}
