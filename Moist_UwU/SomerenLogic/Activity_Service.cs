using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;

namespace SomerenLogic
{
    public class Activity_Service
    {
        Activity_DAO registry_db = new Activity_DAO();

        public void InsertActivity(string Name, string Location, DateTime Dt, string Description)
        {
            try
            {
                registry_db.InsertActivity(Name,Location,Dt,Description);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
