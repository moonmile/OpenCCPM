using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// ダミーの属性
/// </summary>
namespace System.ComponentModel.DataAnnotations
{
    public class KeyAttribute : Attribute {}
    public class TimestampAttribute : Attribute { }
    public class DisplayAttribute : Attribute
    {
        public string Name { get; set; }
    }
    public class TableAttribute : Attribute {
        public TableAttribute(string name) { }
    }
}
namespace System.ComponentModel.DataAnnotations.Schema
{

}
