using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class Drink_Service
    {
        Drinks_DAO drinks_db = new Drinks_DAO();

        public List<Drink> GetDrinks()
        {
            try
            {
                List<Drink> drink = drinks_db.Db_Get_Drinks();
                return drink;
            }
            catch (Exception ex)
            {
                //throw new Exception("Someren couldn't connect to the database");
                return new List<Drink>();
            }

        }
    }
}
