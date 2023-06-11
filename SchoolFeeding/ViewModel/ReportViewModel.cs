using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SchoolFeeding.Model.DatabaseModel;
using SchoolFeeding.Model.Entities;
using SchoolFeeding.Model.Utilities;
using SchoolFeeding.ViewModel.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SchoolFeeding.ViewModel
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly SchoolFeedingContext _context;
        public ICommand ReportingOneCommand { get; }
        public ICommand ReportingThreeCommand { get; }
        private Student student;
        private ObservableCollection<object> date;
        public object SelectedDate;
        public ObservableCollection<object> Date
        {
            get => date;
            set =>SetProperty(ref date, value,nameof(Date));
        }

        public ReportViewModel(Student student)
        {
            _context = new();
            //ReportingCommand = new RelayCommand<object>(Save);
            this.student = student;
        }
        public ReportViewModel()
        {
            _context = new();
            ReportingOneCommand = new RelayCommand<object>(SaveOneMonth);
            ReportingThreeCommand = new RelayCommand<object>(SaveThreeMonth);
        }
        private void SaveOneMonth(object args)
        {
            if (Configure.CurrentUser.Role.Equals("Админ")) GenerateExcelDocument(false);//если мы админ создаём по всем ученикам за промежуток
            else GenerateWordDocument(Configure.CurrentUser.StudentId);
        }
        private void SaveThreeMonth(object args)
        {
            if (Configure.CurrentUser.Role.Equals("Админ")) GenerateExcelDocument(true);//если мы админ создаём по всем ученикам за промежуток
            else GenerateWordDocument(Configure.CurrentUser.StudentId);
        }
        public void GenerateExcelDocument(bool saveMonth)
        {
            string templatePath = $"Отчёт от {DateTime.Now.Date.ToString("dd.MM.yyyy") + " " + DateTime.Now.GetHashCode()}.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(templatePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Отчёт");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "Имя";
                worksheet.Cells[1, 2].Value = "Фамилия";
                worksheet.Cells[1, 3].Value = "Класс";
                worksheet.Cells[1, 4].Value = "Дата";
                worksheet.Cells[1, 5].Value = "Общий минус";
                worksheet.Cells[1, 6].Value = "Баланс";

                int row = 2;

                // Получаем дату, от которой будет производиться отсчет (последний месяц или последние 3 месяца)
                //DateTime startDate = DateTime.Now.Date.AddMonths(-1); // За последний месяц
                DateTime startDate;
                if (saveMonth)
                    startDate = DateTime.Now.Date.AddMonths(-3); // За последние 3 месяца
                else
                    startDate = DateTime.Now.Date.AddMonths(-1);
                foreach (var student in _context.Students.Include(s => s.Payments)
                    .Where(s => s.Payments.Any(p => p.PaymentDate >= startDate))
                    .ToList())
                {
                    decimal OurPayment = 0;
                    foreach (var payment in student.Payments.Where(p => p.PaymentDate >= startDate))
                    {
                        OurPayment += payment.Amount;
                        row++;
                    }
                    worksheet.Cells[row, 1].Value = student.FirstName;
                    worksheet.Cells[row, 2].Value = student.LastName;
                    try
                    {
                        worksheet.Cells[row, 3].Value = _context.Classes.First(x => x.ClassId == student.ClassId).ToString();
                    }
                    catch
                    {
                        worksheet.Cells[row, 3].Value = "Нет данных";
                    }
                    worksheet.Cells[row, 4].Value = DateTime.Now.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 5].Value = "-"+OurPayment;
                    worksheet.Cells[row, 6].Value = _context.Balances.First(x=>x.StudentId==student.StudentId);

                }

                excelPackage.Save();
            }

            MessageBox.Show("Отчёт успешно сохранён");
        }


        public void GenerateWordDocument(int? studentId)
        {
            string templatePath = $"Отчёт от {DateTime.Now.Date.ToString("dd.MM.yyyy") + " " + DateTime.Now.GetHashCode()}.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(templatePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Отчёт");

                worksheet.Cells[1, 1].Value = "Имя";
                worksheet.Cells[1, 2].Value = "Фамилия";
                worksheet.Cells[1, 3].Value = "Класс";
                worksheet.Cells[1, 4].Value = "Дата";
                worksheet.Cells[1, 5].Value = "Общий минус";
                worksheet.Cells[1, 6].Value = "Баланс";

                int row = 2;
                var student = _context.Students.First(x => x.StudentId == studentId);
                decimal OurPayment = 0;
                foreach (var payment in student.Payments)
                {

                    OurPayment += payment.Amount;
                    row++;
                }
                worksheet.Cells[row, 1].Value = student.FirstName;
                worksheet.Cells[row, 2].Value = student.LastName;
                try
                {
                    worksheet.Cells[row, 3].Value = _context.Classes.First(x => x.ClassId == student.StudentId).ToString();
                }
                catch
                {
                    worksheet.Cells[row, 3].Value = "Нет данных";
                }
                worksheet.Cells[row, 4].Value = DateTime.Now.ToString("yyyy-MM-dd");
                worksheet.Cells[row, 5].Value = "-" + OurPayment;
                worksheet.Cells[row, 6].Value = _context.Balances.First(x => x.StudentId == student.StudentId);
                excelPackage.Save();
            }

            MessageBox.Show("Отчёт успешно сохранён");
        }
    }
}

