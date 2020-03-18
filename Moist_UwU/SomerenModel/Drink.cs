using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Drink
    {
        public int ID { get; set; }//drink id

        public int Cost { get; set; }//amount of tokens

        public string Name { get; set; }//name of the drink

        public int Stock { get; set; }//number of drinks in stock

        public int Sold { get; set; }//number of drinks sold
    }
}
