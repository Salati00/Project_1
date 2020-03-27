using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Timetable
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Supervisor { 
            get 
            {
                return $"{SupervisorName} {SupervisorSurname}";
            }
        }
        public string SupervisorName { get; set; }

        public string SupervisorSurname { get; set; }
    }

}
