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
    public class Lecturer_DAO : Base
    {
        public List<Teacher> Db_Get_All_Teachers()
        {
            string query = "SELECT teacher_id, first_name, last_name FROM [teacher]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Teacher> ReadTables(DataTable dataTable)
        {
            List<Teacher> lecturers = new List<Teacher>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Teacher lecturer = new Teacher()
                {
                    Number = (int)dr["teacher_id"],
                    Name = (String)(dr["first_name"].ToString()) + " " + (String)(dr["last_name"].ToString())
                };
                lecturers.Add(lecturer);
            }
            return lecturers;
        }
    }
}
