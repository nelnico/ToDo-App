using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Enums
{
    public class PriorityEnum : Enumeration
    {

        public static PriorityEnum Low = new(1, "Low");
        public static PriorityEnum Medium = new(2, "Medium");
        public static PriorityEnum High = new(3, "High");
        public static PriorityEnum Critical = new(4, "Critical");

        public PriorityEnum(int id, string name)
            : base(id, name, string.Empty)
        {
        }
    }
}
