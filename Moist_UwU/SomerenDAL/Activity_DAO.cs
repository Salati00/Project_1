using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenDAL
{
    public class Activity_DAO : Base
    {
        public void InsertActivity(string Name, string Location, DateTime Dt, string Description)
        {
            try
            {
                string query = "insert into activities(name,location,time,date,description) " +
                "values (@name,@location,@time,@date,@description) ";
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@name", Name);
                sqlParameters[1] = new SqlParameter("@location", Location);
                sqlParameters[2] = new SqlParameter("@time", Dt.TimeOfDay);
                sqlParameters[3] = new SqlParameter("@date", Dt.Date);
                sqlParameters[4] = new SqlParameter("@description", Description);
                ExecuteEditQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
