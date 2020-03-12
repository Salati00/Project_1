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

            string query = "SELECT room_id, room_capacity, room_type FROM [rooms]";
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

    }
}
