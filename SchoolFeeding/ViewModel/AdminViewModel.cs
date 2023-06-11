using Microsoft.EntityFrameworkCore;
using SchoolFeeding.Model.DatabaseModel;
using SchoolFeeding.Model.Dto;
using SchoolFeeding.Model.Entities;
using SchoolFeeding.Model.Utilities;
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
    public class AdminViewModel : ViewModelBase
    {
        private readonly SchoolFeedingContext _context;
        private ObservableCollection<object> dataCollection;
        private object ControlObj;
        private int balance;
        public int Balance
        {
            get => balance;
            set=> SetProperty(ref balance, value,nameof(Balance));
        }
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
            get => selectedItem;
            set
            {
                if (value is null)
                    return;
                if (((StudentDto)value).GetId().Equals(-1))
                {
                    IsAdd = true;
                    IsChange = !IsAdd;

                }
                else
                {
                    IsAdd = false;
                    IsChange = !IsAdd;
                    ControlObj = ((StudentDto)value).GetReferenceObject();
                    FirstName = ((Student)ControlObj).FirstName;
                    LastName = ((Student)ControlObj).LastName;
                    Class = ((StudentDto)value).Класс.ToString();

                }
                SetProperty(ref selectedItem, value, nameof(SelectedItem));
            }
        }
        private string firstName;
        private string lastName;
        private string _class;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value, nameof(FirstName));
        }
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value, nameof(LastName));
        }
        public string Class
        {
            get => _class;
            set => SetProperty(ref _class, value, nameof(Class));
        }
        public bool IsChange
        {
            get { return isChange; }
            set { SetProperty(ref isChange, value, nameof(IsChange)); }
        }
        private bool isAdd;
        public bool IsAdd
        {
            get { return isAdd; }
            set { SetProperty(ref isAdd, value, nameof(IsAdd)); }
        }
        private string summ;
        public string Summ
        {
            get => summ;
            set => SetProperty(ref summ, value, nameof(Summ));
        }
        public ICommand StudentChangeCommand { get; }
        public ICommand StudentAddCommand { get; }
        public ICommand StudentDeleteCommand { get; }
        public ICommand SummCommand { get; }
        public AdminViewModel()
        {
            _context = new();           
            DataCollection = new(GetItem(1));
            StudentChangeCommand = new RelayCommand<object>(ChangeStudent);
            StudentAddCommand = new RelayCommand<object>(AddStudent);
            StudentDeleteCommand = new RelayCommand<object>(DeleteStudent);
            SummCommand = new RelayCommand<object>(SummMethod);
            IsAdd = false;
            IsChange = false;
        }
        private void GetItem()
        {
            var list = new List<StudentDto>();
            foreach (var item in _context.Students.Select(x => x).ToList())
            {
                if (item.FirstName == "Админ")
                    continue;
                else
                    list.Add(new StudentDto(item));
            }
            list.Add(new StudentDto() { Id = -1 });
        }
        private List<StudentDto> GetItem(object args)
        {
            var list = new List<StudentDto>();
            foreach (var item in _context.Students.Select(x => x).ToList())
            {
                if (item.FirstName == "Админ")
                    continue;
                else
                    list.Add(new StudentDto(item));
            }
            list.Add(new StudentDto() { Id = -1 });
            return list;
        }
        private void SummMethod(object args)
        {
            try
            {
                bool isDouble = IsDouble(Summ);
                if (isDouble)
                {
                    var student = _context.Students.Find((ControlObj as Student).StudentId);
                    var balance = _context.Balances.First(x => x.StudentId == student.StudentId);
                    balance.Balance1 = balance.Balance1 + Convert.ToDecimal(Summ);
                    _context.SaveChanges();
                    MessageBox.Show("Оплата успешно внесена");
                    DataCollection = new(GetItem(1));
                }
                else
                {
                    MessageBox.Show("Введенная строка не числом.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private bool IsDouble(string input)
        {
            if (double.TryParse(input, out double result))
                return true;
            else
                return false;
        }
        private void DeleteStudent(object args)
        {
            if (MessageBox.Show("Вы точно хотите удалить этого студента? Произойдёт удаление всей информации связанной с ним", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    var student = _context.Students.Find((ControlObj as Student).StudentId);
                    var balance = _context.Balances.Where(x => x.StudentId == student.StudentId);
                    var payment = _context.Payments.Where(x => x.StudentId == student.StudentId);
                    var user = _context.Users.Where(x => x.StudentId == student.StudentId);

                    foreach (var item in balance)
                        _context.Balances.Remove(item);
                    foreach (var item in payment)
                        _context.Payments.Remove(item);
                    foreach (var item in user)
                        _context.Users.Remove(item);

                    _context.Students.Remove(student);

                    _context.SaveChanges();
                    DataCollection = new(GetItem(1));
                }
                catch (Exception e)
                {
                    Logger.LogError(e.Message);
                }
        }
        private void AddStudent(object args)
        {
            try
            {
                if (_context.Students.Any(x => x.FirstName == FirstName && x.LastName == LastName && x.ClassId == GetClass()))
                {
                    MessageBox.Show("Такой студент уже сущесвует!");
                    return;
                }
                else
                {
                    var newStudent = new Student()
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        ClassId = GetClass()
                    };

                    var newBalance = new Balance()
                    {
                        Balance1 = 0,
                        Student = newStudent
                    };

                    var newPayment = new Payment()
                    {
                        Amount = 0,
                        PaymentDate = DateTime.Now.Date,
                        Student = newStudent
                    };

                    var newUser = new User()
                    {
                        Login = newStudent.FirstName,
                        Password = newStudent.LastName + _context.Classes.First(x => x.ClassId == GetClass()).ToString(),
                        Role = "Ученик",
                        Student = newStudent
                    };

                    _context.Students.Add(newStudent);
                    _context.Balances.Add(newBalance);
                    _context.Payments.Add(newPayment);
                    _context.Users.Add(newUser);

                    _context.SaveChanges();
                }




                _context.SaveChanges();
                DataCollection = new(GetItem(1));
                MessageBox.Show("Добавление прошло успешно");
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
        }
        private void ChangeStudent(object args)
        {
            try
            {
                var student = _context.Students.Find((ControlObj as Student).StudentId);
                if (student is null)
                    return;
                if (_context.Students.Any(x => x.FirstName == FirstName && x.LastName == LastName&& x.ClassId== GetClass()))
                {
                    MessageBox.Show("Такой студент уже сущесвует!");
                    return;
                }
                else
                {
                    student.FirstName = FirstName;
                    student.LastName = LastName;
                    
                    student.ClassId = GetClass();
                    
                }
                _context.SaveChanges();
                DataCollection = new(GetItem(1));
                MessageBox.Show("Сохранение прошло успешно");
            }
            catch(Exception e)
            {
                Logger.LogError(e.Message);
            }
            
        }
        private int GetClass()
        {
            if (Class.Length == 2)
                Class = Class + ' ';
            foreach (var item in _context.Classes.ToList())
            {
                if ((item.ClassCode + item.ClassName).Equals(Class))
                {
                    return item.ClassId;
                }
            }
            return -1;
        }
    }
}
