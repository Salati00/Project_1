using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;

namespace SomerenDAL
{
    public class Timetable_DAO : Base
    {
        public List<Timetable> Get_Timetable()
        {
            string query = "select teacher.first_name as [supervisor name], teacher.last_name as [supervisor surname], [name], location, date " +
                "from activities " +
                "join supervises on activities.activity_id = supervises.activity_id " +
                "join teacher on supervises.teacher_id = teacher.teacher_id " +
                "order by name";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Timetable> ReadTables(DataTable dataTable)
        {
            List<Timetable> Timetables = new List<Timetable>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Timetable timetable = new Timetable()
                {
                    Name = (string)dr["name"],
                    Date = Convert.ToDateTime(dr["date"]),
                    Location = (string)dr["location"],
                    SupervisorName = (string)dr["supervisor name"],
                    SupervisorSurname = (string)dr["supervisor surname"] 
                };
                Timetables.Add(timetable);
            }
            return Timetables;
        }
    }
}
