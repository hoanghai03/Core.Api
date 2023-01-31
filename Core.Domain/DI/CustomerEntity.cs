using Core.Domain.Shared.CustomProperty;
using Core.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DI
{
    [Table("customer",Schema = "public")]
    public class CustomerEntity : BaseEntity
    {
        public Guid id { get; set; }
        [ExportExcel]
        [PropertyName("Tên khách hàng")]
        public string customer_name { get; set; }
        [ExportExcel]
        [PropertyName("Ma khách hàng")]
        public string customer_code { get; set; }
        [ExportExcel]
        [PropertyName("Địa chỉ")]
        public string address { get; set; }
        [ExportExcel]
        [PropertyName("Vị trí")]
        public string position { get; set; }
    }
}
