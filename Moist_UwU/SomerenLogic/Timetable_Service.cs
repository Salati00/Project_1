using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;
using SomerenDAL;

namespace SomerenLogic
{
    public class Timetable_Service
    {
        Timetable_DAO timetable_db = new Timetable_DAO();
        public List<Timetable> GetTimetable()
        {
            try
            {
                List<Timetable> Timetable = timetable_db.Get_Timetable();
                return Timetable;
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }
    }
}
