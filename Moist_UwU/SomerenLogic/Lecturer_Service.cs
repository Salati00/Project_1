using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class Lecturer_Service
    {
        Lecturer_DAO teacher_db = new Lecturer_DAO();

        public List<Teacher> GetTeachers()
        {
                List<Teacher> lecturer = teacher_db.Db_Get_All_Teachers();
                return lecturer;
        }
    }
}
