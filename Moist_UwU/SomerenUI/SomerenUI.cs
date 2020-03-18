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

            //CODE TO INITIALIZE 225 rooms in the DB || NEVER CALL AGAIN UNLESS NECESSARY
            /*Room_DAO r = new Room_DAO();
            r.InitializeRooms();*/
        }

        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void HideAll()
        {
            pnl_Students.Hide();
            pnl_Rooms.Hide();
            pnl_Lec.Hide();
            Pnl_Activities.Hide();
            pnl_Dashboard.Hide();
            img_Dashboard.Hide();
        }

        private void showPanel(string panelName)
        {

            if(panelName == "Dashboard")
            {
                HideAll();

                // show dashboard
                pnl_Dashboard.Show();
                img_Dashboard.Show();
            }
            else if(panelName == "Students")
            {
                HideAll();

                // show students
                pnl_Students.Show();

                // fill the students listview within the students panel with a list of students
                Student_Service studService = new Student_Service();
                List<Student> studentList = studService.GetStudents();

                ListViewStuPrint(listViewStudents, studentList);
            }
            else if (panelName == "Lecturer")
            {
                HideAll();

                // show rooms
                pnl_Lec.Show();

                // fill the lecturer listview within the lectuer panel with a list of lecturers
                Lecturer_Service lecService = new Lecturer_Service();
                List<Teacher> lecList = lecService.GetTeachers();

                ListViewLecPrint(listView_Lec, lecList);

            }
            else if (panelName == "Rooms")
            {
                HideAll();

                // show rooms
                pnl_Rooms.Show();

                // fill the rooms listview within the rooms panel with a list of rooms
                Room_Service roomService = new Room_Service();
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
                var row = new string[] { s.Number.ToString(), s.FirstName, s.LastName, s.phoneNumber};
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

        private void activitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Activities");
        }
    }
}
