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
        public string student_code { get; set; }
        public string student_name{ get; set; }
        public int? age { get; set; }
    }
}
