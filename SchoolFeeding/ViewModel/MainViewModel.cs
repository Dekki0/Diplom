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
        #region public block
        public object Page { get; set; }
        public MainViewModel()
        {
            StackControl.DataChanged += HandleDataChanged;
            Page = new MenuViewModel();
        }
        #endregion

        #region private methods
        private void HandleDataChanged(object newData) => Page = newData;
        #endregion 
    }
}
