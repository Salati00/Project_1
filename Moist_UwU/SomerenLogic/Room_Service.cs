﻿using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenLogic
{
    public class Room_Service
    {

        Room_DAO room_db = new Room_DAO();

        public List<Room> GetRooms()
        {
            /*try
            {*/
                List<Room> rooms = room_db.Db_Get_All_Rooms();
                return rooms;
            /*}*/
            /*catch (Exception ex)
            {
                /*
                // something went wrong connecting to the database, so we will add a fake student to the list to make sure the rest of the application continues working!
                List<Student> student = new List<Student>();
                Student a = new Student();
                a.Name = "Mr. Test Student";
                a.Number = 474791;
                a.BirthDate = DateTime.Parse("1990-07-04");
                student.Add(a);
                Student b = new Student();
                b.Name = "Mrs. Test Student";
                b.Number = 197474;
                b.BirthDate = DateTime.Parse("2019-03-04");
                student.Add(b);
                return student;
                //throw new Exception("Someren couldn't connect to the database");



            }
            return new List<Room>();*/


        }
    }
}
