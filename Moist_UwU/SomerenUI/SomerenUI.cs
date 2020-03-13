using SomerenLogic;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SomerenDAL;


namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();

            //CODE TO INITIALIZE 225 rooms in the DB
            /*Room_DAO ndaokjsfnewsi = new Room_DAO();
            ndaokjsfnewsi.InitializeRooms();*/
        }

        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void showPanel(string panelName)
        {

            if(panelName == "Dashboard")
            {

                // hide all other panels
                pnl_Students.Hide();
                pnl_Rooms.Hide();
                pnl_Lec.Hide();

                // show dashboard
                pnl_Dashboard.Show();
                img_Dashboard.Show();
            }
            else if(panelName == "Students")
            {
                // hide all other panels
                pnl_Dashboard.Hide();
                img_Dashboard.Hide();
                pnl_Lec.Hide();
                pnl_Rooms.Hide();

                // show students
                pnl_Students.Show();

                

                // fill the students listview within the students panel with a list of students
                SomerenLogic.Student_Service studService = new SomerenLogic.Student_Service();
                List<Student> studentList = studService.GetStudents();

                ListViewStuPrint(listViewStudents, studentList);
            }
            else if (panelName == "Lecturer")
            {
                // hide all other panels
                pnl_Dashboard.Hide();
                img_Dashboard.Hide();
                pnl_Students.Hide();
                pnl_Rooms.Hide();

                // show rooms
                pnl_Lec.Show();



                // fill the lecturer listview within the lectuer panel with a list of lecturers
                SomerenLogic.Lecturer_Service lecService = new SomerenLogic.Lecturer_Service();
                List<Teacher> lecList = lecService.GetTeachers();

                ListViewLecPrint(listView_Lec, lecList);

            }
            else if (panelName == "Rooms")
            {
                // hide all other panels
                pnl_Dashboard.Hide();
                img_Dashboard.Hide();
                pnl_Students.Hide();
                pnl_Lec.Hide();

                // show rooms
                pnl_Rooms.Show();



                // fill the rooms listview within the rooms panel with a list of rooms
                SomerenLogic.Room_Service roomService = new SomerenLogic.Room_Service();
                List<Room> roomList = roomService.GetRooms();

                ListViewRoomPrint(listViewRooms, roomList);
            }
        }

        //Creating List view for teachers with 2 columns
        public void ListViewLecPrint(ListView lv, List<Teacher> lecList)
        {
            lv.Items.Clear();
            foreach (Teacher t in lecList)
            {
                var row = new string[] { t.Number.ToString(), t.Name };
                var lvi = new ListViewItem(row);
                lv.Items.Add(lvi);
            }
        }

        //Creating List view for students with 2 columns
        public void ListViewStuPrint(ListView lv, List<Student> stuList)
        {
            lv.Items.Clear();
            foreach (Student s in stuList)
            {
                var row = new string[] { s.Number.ToString(), s.Name, s.BirthDate.Date.ToString("dd/MM/yyyy") };
                var lvi = new ListViewItem(row);
                lv.Items.Add(lvi);
            }
        }

        //Creating List view for rooms 
        public void ListViewRoomPrint(ListView lv, List<Room> rooms)
        {

            lv.Items.Clear();
            string typename = "";
            foreach (Room r in rooms)
            {
                if (r.Type)
                {
                    typename = "Student Room";
                }
                else
                {
                    typename = "Teacher Room";
                }
                var row = new string[] { r.Number.ToString(), r.Capacity.ToString(), typename };
                var lvi = new ListViewItem(row);
                lv.Items.Add(lvi);
            }
            
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }


        private void img_Dashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Students");
        }

        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Rooms");
        }

        private void lecturersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Lecturer");
        }
    }
}
