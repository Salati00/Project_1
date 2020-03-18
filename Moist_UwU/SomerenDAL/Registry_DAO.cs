using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenDAL
{
    public class Registry_DAO: Base
    {

        public void InsertSale(int studentid, int drinkid)
        {
            string query = "INSERT INTO purchases (student_id,drink_id) VALUES (@stu,@drink)";
            
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@stu", studentid.ToString());
            sqlParameters[1] = new SqlParameter("@drink", drinkid.ToString());
            base.ExecuteInsertQuery(query, sqlParameters);
            query = "UPDATE DRINKS SET stock = stock - 1, sold = sold + 1" +
                " where drink_id = @id";
            sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", drinkid.ToString());
            base.ExecuteInsertQuery(query, sqlParameters);
        }

    }
}
