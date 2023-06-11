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
using System.Windows;
using System.Windows.Input;

namespace SchoolFeeding.ViewModel
{
    public class VisitsViewModel : ViewModelBase
    {
        #region private varible
        
        private readonly SchoolFeedingContext context;
        private ObservableCollection<object> classes;
        private ObservableCollection<object> students;
        private object student;
        private object @class;

        #endregion

        #region public property
        
        public ICommand ChDayCommand { get; }
        public ICommand TodayCommand { get; }
        public ObservableCollection<object> Classes 
        {
            get => classes;
            set => SetProperty(ref classes, value,nameof(Classes));
        }
        public ObservableCollection<object> Students
        {
            get => students;
            set => SetProperty(ref students, value, nameof(Students));
        }
        public object SelectedStudent
        {
            get=> student;
            set => SetProperty(ref student, value, nameof(SelectedStudent));
        }

        public string CurrentDate { get; set; }
        public object SelectedClass
        {
            get => @class;
            set 
            {
                SetProperty(ref @class, value, nameof(SelectedClass));
                GetStudents((value as Class).ClassId);
            }
        }

        #endregion

        public VisitsViewModel()
        {
            context = new();
            CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            Classes = new(context.Classes.ToList());
            SelectedClass = Classes.First();
            TodayCommand = new RelayCommand<object>(CheckOut);
        }
        private void GetStudents(int classId)
        {
            Students = new();
            foreach (var item in context.Students.Where(x => x.ClassId == classId).ToList())
            {
                if(context.Visits.Any(x=>x.StudentId==item.StudentId))
                    Students.Add(new ListStudent(item,true));
                else
                    Students.Add(new ListStudent(item));
            }
               
        }
        public void CheckOut(object sender)
        {
            foreach (var student in Students.Where(s => (s as ListStudent).IsSelected))
            {
                (student as ListStudent).AddVisit();
            }
            MessageBox.Show("Отсутсвующие ученики успешно отмечены");
        }
    }
}
