using SchoolFeeding.Model.DatabaseModel;
using SchoolFeeding.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFeeding.Model.Dto
{
    class StudentDto
    {
        private readonly SchoolFeedingContext context;
        public int Id { private get; set; }
        public string Имя { get; set; }
        public string Фамилия { get; set; }
        public Class Класс { get; set; }

        public StudentDto(Student student) 
        {
            context = new();
            Id = student.StudentId;
            Имя = student.FirstName;
            Фамилия = student.LastName;
            Класс = context.Classes.Where(x=>x.ClassId.Equals(student.ClassId)).First();
        }
        public StudentDto()
        {

        }
    }
}
