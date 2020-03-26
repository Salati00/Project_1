using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class Activity_Service
    {
        Activity_DAO activity_db = new Activity_DAO();

        public void InsertActivity(string Name, string Location, DateTime Dt, string Description)
        {
            try
            {
                activity_db.InsertActivity(Name,Location,Dt,Description);
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
                activity_db.UpdateActivity(ID, Name, Location, Dt, Description);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Activity> GetActivities()
        {
            try
            {
                List<Activity> activities = activity_db.Get_Activities();
                return activities;
            }
            catch (Exception ex)
            {
                //throw new Exception("Someren couldn't connect to the database");
                return new List<Activity>();
            }
        }

        public void RemoveActivity(int id)
        {
            try
            {
                activity_db.DeleteActivity(id);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
