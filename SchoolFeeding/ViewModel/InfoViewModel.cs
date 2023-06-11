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
        
        public string Minus { get; set; }
        public string Balance { get; set; }
        public string CurrentDate { get; set; }
        public InfoViewModel()
        {
            if (Configure.CurrentUser.Role == "Админ")
                return;
            _context = new SchoolFeedingContext();
            decimal min = 0;
            foreach(var item in _context.Payments.Where(x=>x.StudentId==Configure.CurrentUser.StudentId))
                min += item.Amount;

            Minus = "-" + min.ToString();
            Balance = _context.Balances.First(x => x.StudentId == Configure.CurrentUser.StudentId).Balance1.ToString();
            CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
