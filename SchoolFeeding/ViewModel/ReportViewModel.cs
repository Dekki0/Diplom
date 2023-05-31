using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SchoolFeeding.Model.Entities;
using SchoolFeeding.ViewModel.Services;
using System;
using System.Windows.Input;

namespace SchoolFeeding.ViewModel
{
    public class ReportViewModel
    {
        public ICommand ReportingCommand { get; }
        private Student saveStudent { get;set; }
        private Payment savePayment { get; set; }
        private Class saveClass {  get; set; }
        public ReportViewModel(Student s, Payment p, Class c) 
        {
            saveClass = c;
            saveStudent = s;
            savePayment = p;
            ReportingCommand = new RelayCommand<object>(Save);
        }
        private void Save(object args)=>
            GenerateWordDocument(saveStudent, savePayment, saveClass);
        public static void GenerateWordDocument(Student student, Payment payment,Class @class)
        {
            string templatePath = $"Отчёт от {DateTime.Now}.docx";

            using (WordprocessingDocument wordDocument = WordprocessingDocument.CreateFromTemplate(templatePath))
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

                TableRow dataRow = new();
                dataRow.Append(
                    CreateTableDataCell(student.FirstName),
                    CreateTableDataCell(student.LastName),
                    CreateTableDataCell(@class.ClassCode+ @class.ClassName),
                    CreateTableDataCell(payment.PaymentDate.ToString("dd.MM.yyyy")),
                    CreateTableDataCell(payment.Amount.ToString())
                ); 

                table.Append(dataRow);

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

