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
                List<Drink> drink = drinks_db.Get_Drinks();
                return drink;
            }
            catch (Exception ex)
            {
                //throw new Exception("Someren couldn't connect to the database");
                return new List<Drink>();
            }

        }

        public bool UpdateDrink(int id, string Name, int Stock)
        {
            try
            {
                drinks_db.UpdateDrink(id,Name,Stock);
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception("Someren couldn't connect to the database");
                return false;
            }
        }

        public bool AddDrink(string Name, int Stock, int Sold, int Price)
        {
            try
            {
                drinks_db.AddDrink(Name, Stock, Sold, Price);
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception("Someren couldn't connect to the database");
                return false;
            }
        }
    }
}
