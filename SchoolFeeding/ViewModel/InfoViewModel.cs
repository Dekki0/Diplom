using Microsoft.EntityFrameworkCore;
using SchoolFeeding.Model.DatabaseModel;
using SchoolFeeding.Model.Utilities;
using SchoolFeeding.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFeeding.ViewModel
{
    public class InfoViewModel : ViewModelBase
    {
        private readonly SchoolFeedingContext _context;
        private ObservableCollection<object> paymentsData;
        public ObservableCollection<object> PaymentsData 
        {
            get => paymentsData;
            set => SetProperty(ref paymentsData, value, nameof(PaymentsData));
        }
        public InfoViewModel()
        {
            _context = new SchoolFeedingContext();
            PaymentsData = new ObservableCollection<object>(
                _context.Payments
                    .Where(student=>student.StudentId.Equals(Configure.CurrentUser.StudentId)).ToList());
        }
    }
}
