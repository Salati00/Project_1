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
    public class Drinks_DAO : Base
    {
        public List<Drink> Db_Get_Drinks()
        {
            string query = "SELECT drink_id, cost, name, stock, sold FROM [drinks] " +
                "where " +
                "cost > 1 and name not in('Water','Orangeade','Cherry juice') " +
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
                    Sold = Convert.ToInt32(Convert.IsDBNull(dr["sold"])?0: Convert.ToInt32(dr["sold"]))
                };
                Drinks.Add(drink);
            }
            return Drinks;
        }

        public void UpdateDrink(int id, string Name, int Stock)
        {
            string query = "update drinks " +
                "set[name] = @name, stock = @stock " +
                "where drink_id = @id";
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@name", Name);
            sqlParameters[1] = new SqlParameter("@stock", Stock);
            sqlParameters[2] = new SqlParameter("@id", id);
            ExecuteEditQuery(query, sqlParameters);
        }
    }
}
