using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class Register_Service
    {
        Registry_DAO registry_db = new Registry_DAO();

        public List<Drink> GetDrinks()
        {
            try
            {
                List<Drink> drink = registry_db.Db_Get_Drinks();
                return drink;
            }
            catch (Exception ex)
            {
                //throw new Exception("Someren couldn't connect to the database");
                return new List<Drink>();
            }

        }

        public void InsertSale(int studentid, int drinkid)
        {
            try
            {
                registry_db.InsertSale(studentid, drinkid);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
