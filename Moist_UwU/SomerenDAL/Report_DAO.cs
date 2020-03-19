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
    public class Report_DAO : Base
    {
        public List<Report> Db_Get_All_Reports()
        {
            string query = "select student_id,  [Purchase Date], drinks.cost from purchases, drinks WHERE purchases.drink_id = drinks.drink_id ";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        private List<Report> ReadTables(DataTable dataTable)
        {
            List<Report> Reports = new List<Report>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Report report = new Report()
                {
                    Sales = 1,
                    CustomerID = Convert.ToInt32(dr["student_id"]),
                    Turnover = Convert.ToInt32(dr["cost"]),
                    Purchase_Date = Convert.ToDateTime(dr["Purchase Date"])
                };
                Reports.Add(report);
            }
            return Reports;
        }
    }
}
