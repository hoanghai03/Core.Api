using Core.Domain.Shared.CustomProperty;
using Core.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DI
{
    [Table("student",Schema = "public")]
    public class StudentEntity : BaseEntity
    {
        public Guid id { get; set; }
        [ExportExcel]
        [PropertyName("Ma sinh viên")]
        public string student_code { get; set; }

        [ExportExcel]
        [PropertyName("Tên sinh viên")]
        public string student_name{ get; set; }

        [ExportExcel]
        [PropertyName("Tuổi")]
        public int? age { get; set; }
    }
}
