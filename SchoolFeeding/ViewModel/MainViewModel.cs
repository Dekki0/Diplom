using SchoolFeeding.Model.DatabaseModel;
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
        private readonly SchoolFeedingContext _context;

        #endregion

        #region public block
        public object Page
        {
            get => page ?? new MenuViewModel();
            private set => SetProperty(ref page, value, nameof(Page));
        }
        public ICommand BackPageCommand { get; }
        public MainViewModel()
        {
            _context = new();
            StackControl.DataChanged += HandleDataChanged;
            StackControl.AddPage(new MenuViewModel());
            BackPageCommand = new RelayCommand<object>(BackPage);
            SubtractAmountForDaysSinceLastPayment();
        }

        #endregion

        #region private methods
        private void BackPage(object args) => Page = StackControl.GetPage() ?? new MenuViewModel();
        private void HandleDataChanged(object newData) => Page = newData;
        private void SubtractAmountForDaysSinceLastPayment()
        {
            DateTime currentDate = DateTime.Today;
            List<Student> students = _context.Students.ToList();
            foreach (Student student in students)
            {
                DateTime lastPaymentDate = _context.Payments
                    .Where(p => p.StudentId == student.StudentId)
                    .OrderByDescending(p => p.PaymentDate)
                    .Select(p => p.PaymentDate)
                    .FirstOrDefault();
                if (lastPaymentDate == DateTime.MinValue)
                    continue;
                int daysSinceLastPayment = (int)(currentDate - lastPaymentDate).TotalDays;
                for (int i = 1; i < daysSinceLastPayment; i++)
                {
                    DateTime currentDateMinusDays = currentDate.AddDays(-i);
                    if (currentDateMinusDays.DayOfWeek != DayOfWeek.Saturday && currentDateMinusDays.DayOfWeek != DayOfWeek.Sunday)
                    {
                        bool isVisitExists = _context.Visits
                                .Any(v => v.StudentId == student.StudentId && v.DateVisit.Date == currentDateMinusDays.Date);
                        if (!isVisitExists)
                        {
                            Balance studentBalance = _context.Balances.FirstOrDefault(b => b.StudentId == student.StudentId);
                            if (studentBalance != null)
                            {
                                studentBalance.Balance1 -= 3;
                                Payment newPayment = new Payment
                                {
                                    StudentId = student.StudentId,
                                    Amount = 3,
                                    PaymentDate = currentDateMinusDays
                                };
                                _context.Payments.Add(newPayment);
                            }
                        }
                    }
                }

            }
            _context.SaveChanges();
        }


        #endregion 
    }
}
