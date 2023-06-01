using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SchoolFeeding.Model.DatabaseModel;
using SchoolFeeding.Model.Entities;
using SchoolFeeding.Model.Utilities;
using SchoolFeeding.ViewModel.Services;
using System;
using System.Linq;
using System.Windows.Input;

namespace SchoolFeeding.ViewModel
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly SchoolFeedingContext _context;
        public ICommand ReportingCommand { get; }
        private Student saveStudent { get;set; }
        private DateTime startDate { get; set; }        
        private DateTime endDate { get; set; }

        public ReportViewModel() 
        {
            _context = new();
            startDate = new DateTime(2023,5,1);
            endDate = DateTime.Now; //привяжи к выбранным датам если не в падлу
            ReportingCommand = new RelayCommand<object>(Save);
        }
        private void Save(object args)
        {
            if (Configure.CurrentUser.Role.Equals("Admin")) GenerateWordDocument();//если мы админ создаём по всем ученикам за промежуток
            else GenerateWordDocument(Configure.CurrentUser.Student);
        }
        public void GenerateWordDocument()
        {
            string templatePath = $"Отчёт от {DateTime.Now}.docx";

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create("C:\\Users\\egorb\\source\\repos\\SchoolFeeding\\SchoolFeeding", WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.MainDocumentPart;
                Body body = mainPart.Document.Body;

                Table table = new Table();

                TableRow headerRow = new TableRow();
                headerRow.Append(
                    CreateTableHeaderCell("Имя"),
                    CreateTableHeaderCell("Фамилия"),
                    CreateTableHeaderCell("Класс"),
                    CreateTableHeaderCell("Дата оплаты"),
                    CreateTableHeaderCell("Сумма оплаты")
                );

                table.Append(headerRow);

                foreach (var payment in _context.Payments.Select(x => x).
                    Where(x => x.PaymentDate > startDate && x.PaymentDate < endDate).ToList())
                {
                    TableRow dataRow = new();
                    dataRow.Append(
                        CreateTableDataCell(payment.Student.FirstName),
                        CreateTableDataCell(payment.Student.LastName),
                        CreateTableDataCell(payment.Student.Class.ClassCode + payment.Student.Class.ClassName),
                        CreateTableDataCell(payment.PaymentDate.ToString("dd.MM.yyyy")),
                        CreateTableDataCell(payment.Amount.ToString())
                    );

                    table.Append(dataRow);
                }

                body.Append(table);

                mainPart.Document.Save();
            }
        }

        public void GenerateWordDocument(Student student)
        {
            string templatePath = $"Отчёт {student.LastName} от {DateTime.Now}.docx";

            using (WordprocessingDocument wordDocument = WordprocessingDocument.CreateFromTemplate(templatePath))
            {
                MainDocumentPart mainPart = wordDocument.MainDocumentPart;
                Body body = mainPart.Document.Body;

                Table table = new Table();

                TableRow headerRow = new TableRow();
                headerRow.Append(
                    CreateTableHeaderCell("Дата оплаты"),
                    CreateTableHeaderCell("Сумма оплаты")
                );

                table.Append(headerRow);

                foreach (var payment in _context.Payments.Select(x => x).
                                    Where(x => x.PaymentDate > startDate && x.PaymentDate < endDate).ToList())
                {
                    TableRow dataRow = new();
                    dataRow.Append(
                        CreateTableDataCell(payment.PaymentDate.ToString("dd.MM.yyyy")),
                        CreateTableDataCell(payment.Amount.ToString())
                    );

                    table.Append(dataRow);
                }

                body.Append(table);

                mainPart.Document.Save();
            }
        }

        private static TableCell CreateTableHeaderCell(string text)
        {
            TableCell cell = new TableCell(new Paragraph(new Run(new Text(text))));
            cell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
            return cell;
        }

        private static TableCell CreateTableDataCell(string text)
        {
            TableCell cell = new TableCell(new Paragraph(new Run(new Text(text))));
            cell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
            return cell;
        }
    }
}

