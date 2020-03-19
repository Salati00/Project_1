using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class Report_Service
    {
        Report_DAO report_db = new Report_DAO();

        public Report GetReport(DateTime from, DateTime until)
        {
            List<Report> reports = report_db.Db_Get_All_Reports();
            List<int> ID = new List<int>();
            int turnover = 0;
            int sales = 0;
            foreach (Report r in reports)
            {
                if (r.Purchase_Date >= from && r.Purchase_Date <= until)
                {
                    if (!ID.Contains(r.CustomerID))
                    {
                        ID.Add(r.CustomerID);
                    }
                    turnover += r.Turnover;
                    sales += r.Sales;
                }
            }
            Report report = new Report();
            report.CustomerID = ID.Count;
            report.Turnover = turnover;
            report.Sales = sales;
            return report;
        }

        public Report GetReport()
        {
            List<Report> reports = report_db.Db_Get_All_Reports();
            List<int> ID = new List<int>();
            int turnover = 0;
            int sales = 0;
            foreach (Report r in reports)
            {
                if (!ID.Contains(r.CustomerID))
                {
                    ID.Add(r.CustomerID);
                }
                turnover += r.Turnover;
                sales += r.Sales;
            }
            Report report = new Report();
            report.CustomerID = ID.Count;
            report.Turnover = turnover;
            report.Sales = sales;
            return report;
        }
    }
}
