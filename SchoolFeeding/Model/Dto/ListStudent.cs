using SchoolFeeding.Model.DatabaseModel;
using SchoolFeeding.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFeeding.Model.Dto
{
    class ListStudent : INotifyPropertyChanged
    {
        private readonly SchoolFeedingContext context;
        public int Id { private get; set; }
        public string Имя { get; set; }
        public string Фамилия { get; set; }
        public Class Класс { get; set; }
        public Balance Баланс { get; set; }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }
        private Student referenceObj;

        public ListStudent(Student student)
        {
            context = new();
            Id = student.StudentId;
            Имя = student.FirstName;
            Фамилия = student.LastName;
            Класс = context.Classes.Where(x => x.ClassId.Equals(student.ClassId)).First();
            referenceObj = student;
            Баланс = context.Balances.Where(x => x.StudentId.Equals(student.StudentId)).FirstOrDefault() ?? new Balance() { Balance1 = 0 };
        }
        public ListStudent(Student student,bool visit)
        {
            context = new();
            IsSelected = visit;
            Id = student.StudentId;
            Имя = student.FirstName;
            Фамилия = student.LastName;
            Класс = context.Classes.Where(x => x.ClassId.Equals(student.ClassId)).First();
            referenceObj = student;
            Баланс = context.Balances.Where(x => x.StudentId.Equals(student.StudentId)).FirstOrDefault() ?? new Balance() { Balance1 = 0 };
        }
        public ListStudent()
        {

        }
        public int GetId() =>
            Id;
        public Student GetReferenceObject() =>
            referenceObj;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AddVisit()
        {
            using (var context = new SchoolFeedingContext())
            {
                var visit = new Visit
                {
                    StudentId = Id,
                    DateVisit = DateTime.Now.Date
                };

                context.Visits.Add(visit);
                context.SaveChanges();
            }
        }
    }
}
