using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Serials : BaseEntity
    {
        public int Serial_Id { get; set; }
        public string SerialName { get; set; }
        public string SerialPrefix { get; set; }
        public Int64 SerialCounter { get; set; }
        public DateTime DTS { get; set; }
    }
}
