﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Student
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; } 
        public string phoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return FirstName +" "+ LastName;
            }
        }
    }
}
