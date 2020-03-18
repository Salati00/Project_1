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
    public class Room_DAO : Base
    {

        public List<Room> Db_Get_All_Rooms()
        {

            string query = "SELECT room_id, room_capacity, room_type FROM [rooms] WHERE room_id > 200 AND room_id < 226";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }

        private List<Room> ReadTables(DataTable dataTable)
        {
            List<Room> Rooms = new List<Room>();

            foreach (DataRow dr in dataTable.Rows)
            {
                bool yeh;
                if (dr["room_type"].ToString() == "st")
                {
                    yeh = true;
                }
                else
                {
                    yeh = false;
                }

                Room room = new Room()
                {
                    Number = (int)dr["room_id"],
                    Capacity = (int)dr["room_capacity"],
                    Type = yeh
                };
                Rooms.Add(room);
            }
            return Rooms;
        }

        /* Enters 225 random rooms into the database*/
        public void InitializeRooms()
        {
            string query = "INSERT INTO rooms (room_capacity,room_type) VALUES (@cap,@type)";
            Random rnd = new Random();

            for (int i = 0; i < 225; i++)
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                int r = rnd.Next(1, 4) * 5;
                sqlParameters[0] = new SqlParameter("@cap", r.ToString());
                sqlParameters[1] = new SqlParameter("@type", ((rnd.Next(1, 3) == 1) ? "st" : "te"));
                base.ExecuteInsertQuery(query, sqlParameters);
            }
        }

        

    }
}
