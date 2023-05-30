using SchoolFeeding.Model.DatabaseModel;
using SchoolFeeding.Model.Dto;
using SchoolFeeding.Model.Entities;
using SchoolFeeding.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFeeding.ViewModel
{
    public class AdminViewModel: ViewModelBase
    {
        private readonly SchoolFeedingContext _context;
		private ObservableCollection<object> dataCollection;
		public ObservableCollection<object> DataCollection
        {
			get => dataCollection;
            set
            {
                SetProperty(ref dataCollection, value, nameof(DataCollection));
            }
		}
        private bool isChange;
        private object selectedItem;
        public object SelectedItem
        {
            get =>selectedItem;
            set 
            {
                if (value == new StudentDto())
                {
                    isAdd = true;
                    isChange = false;
                }
                else
                {
                    isAdd = false;
                    isChange = true;
                    SetProperty(ref selectedItem, value, nameof(SelectedItem));
                }
            }
        }
        public bool IsChange
        {
            get { return isChange; }
            set { SetProperty(ref isChange,value,nameof(IsChange)); }
        }
        private bool isAdd;

        public bool IsAdd
        {
            get { return isAdd; }
            set { SetProperty(ref isAdd, value, nameof(IsAdd)); }
        }
        public AdminViewModel()
        {
            _context = new();
            var list = new List<StudentDto>();
            foreach (var item in _context.Students.Select(x=>x).ToList())
                list.Add(new StudentDto(item));
            list.Add(new StudentDto());
            DataCollection = new(list);

            IsAdd = false;
            IsChange = false;
        }
    }
}
