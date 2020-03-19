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
    public class Registry_DAO : Base
    {

        public void InsertSale(int studentid, int drinkid)
        {
            string query = "INSERT INTO purchases (student_id,drink_id,[Purchase Date]) VALUES (@stu,@drink,@date)";
            
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@stu", studentid.ToString());
            sqlParameters[1] = new SqlParameter("@drink", drinkid.ToString());
            sqlParameters[2] = new SqlParameter("@date", DateTime.Today);
            base.ExecuteNonQuery(query, sqlParameters);
            query = "UPDATE DRINKS SET stock = stock - 1, sold = sold + 1" +
                " where drink_id = @id";
            sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", drinkid.ToString());
            base.ExecuteNonQuery(query, sqlParameters);
        }

        public List<Drink> Db_Get_Drinks()
        {
            string query = "SELECT drink_id, cost, name, stock, sold FROM [drinks] " +
                "order by stock, cost, sold";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Drink> ReadTables(DataTable dataTable)
        {
            List<Drink> Drinks = new List<Drink>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Drink drink = new Drink()
                {
                    ID = (int)dr["drink_id"],
                    Name = dr["name"].ToString(),
                    Cost = Convert.ToInt32(dr["cost"]),
                    Stock = Convert.ToInt32(dr["stock"]),
                    Sold = Convert.ToInt32(Convert.IsDBNull(dr["sold"]) ? 0 : Convert.ToInt32(dr["sold"]))
                };
                Drinks.Add(drink);
            }
            return Drinks;
        }

    }
}
