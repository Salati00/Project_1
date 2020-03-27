using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    public class Supervisors_DAO : Base
    {
            public List<Supervisor> Db_Get_All_Supervisors()
            {
                string query = "SELECT supervise_id, first_name, last_name, name AS [activity_name]" +
                "FROM supervises join teacher ON supervises.teacher_id = teacher.teacher_id " +
                "JOIN activities ON activities.activity_id = supervises.activity_id";
                SqlParameter[] sqlParameters = new SqlParameter[0];
                return ReadTables(ExecuteSelectQuery(query, sqlParameters));
            }

            private List<Supervisor> ReadTables(DataTable dataTable)
            {
                List<Supervisor> supervisors = new List<Supervisor>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    Supervisor supervisor = new Supervisor()
                    {
                        Name = dr["first_name"].ToString() + " " + dr["last_name"].ToString(),
                        ActivityName = (string)dr["activity_name"],
                        SuperviseID = (int)dr["supervise_id"],
                    };
                    supervisors.Add(supervisor);
                }
                return supervisors;
            }
        public void RemoveSupervisor(int id)
        {
            string query = "DELETE FROM supervises " +
                "WHERE supervise_id = @id";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", id);
            ExecuteNonQuery(query, sqlParameters);
        }
        private bool CheckExists(int id)
        {
            string query = "SELECT teacher_id FROM [supervises] " +
                "where [teacher_id] = @id";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", id);
            DataTable dt = ExecuteSelectQuery(query, sqlParameters);
            return dt.Rows.Count != 0;
        }
        public void InsertSupervisor(int teach_id, int activity)
        {
            if (CheckExists(teach_id))
                throw new Exception("Teacher is already a Supervisor");

            string query = "insert into supervises(teacher_id,activity_id) " +
            "values ( @teacher, @activity) ";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@teacher", teach_id);
            sqlParameters[1] = new SqlParameter("@activity", activity);
                ExecuteEditQuery(query, sqlParameters);
        }

    }
}
