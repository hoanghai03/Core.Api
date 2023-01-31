using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.CustomProperty
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportExcel : Attribute
    {
        public ExportExcel() { }
    }    
    
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string _name;
        public PropertyName(string name) {
            _name= name;
        }
    }
}
