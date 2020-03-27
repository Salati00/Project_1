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
    public class Activity_DAO : Base
    {
        public void InsertActivity(string Name, string Location, DateTime Dt, string Description)
        {
            try
            {
                if (checkExists(Name))
                    throw new Exception("Activity already present");
                Dt = Dt.AddSeconds(-Dt.Second);

                string query = "insert into activities(name,location,date,description) " +
                "values (@name,@location,@date,@description) ";
                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@name", Name);
                sqlParameters[1] = new SqlParameter("@location", Location);
                sqlParameters[2] = new SqlParameter("@date", Dt);
                sqlParameters[3] = new SqlParameter("@description", Description);
                ExecuteEditQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateActivity(int ID, string Name, string Location, DateTime Dt, string Description)
        {
            try
            {
                Dt = Dt.AddSeconds(-Dt.Second);

                string query = "Update activities " +
                    "set name = @name, location = @location, date = @date, description = @description " +
                    "where activity_id = @id";
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@name", Name);
                sqlParameters[1] = new SqlParameter("@location", Location);
                sqlParameters[2] = new SqlParameter("@date", Dt);
                sqlParameters[3] = new SqlParameter("@description", Description);
                sqlParameters[4] = new SqlParameter("@id", ID);
                ExecuteEditQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool checkExists(string Name)
        {
            string query = "SELECT * FROM [activities] " +
                "where [name] = @name";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@name", Name);
            DataTable dt = ExecuteSelectQuery(query, sqlParameters);
            return dt != null;
        }

        public List<Activity> Get_Activities()
        {
            string query = "SELECT activity_id, name, location, date, description FROM [activities] " +
                "order by name";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Activity> ReadTables(DataTable dataTable)
        {
            List<Activity> Activities = new List<Activity>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Activity activity = new Activity()
                {
                    ID = (int)dr["activity_id"],
                    Name = (string)dr["name"],
                    Location = (string)dr["location"],
                    Date = Convert.ToDateTime(dr["date"]),
                    Description = (string)dr["description"]
                };
                Activities.Add(activity);
            }
            return Activities;
        }

        public void DeleteActivity(int id)
        {
            string query = "Delete from activities " +
                "where activity_id = @id";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", id);
            ExecuteNonQuery(query, sqlParameters);
        }
    }
}
