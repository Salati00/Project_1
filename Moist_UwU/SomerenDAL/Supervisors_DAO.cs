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
                string query = "SELECT supervise_id, first_name, last_name, activity_id FROM supervises	join teacher ON supervises.teacher_id = teacher.teacher_id";
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
                        ActivityID = (int)dr["activity_id"],
                        SuperviseID = (int)dr["supervise_id"]
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

    }
}
