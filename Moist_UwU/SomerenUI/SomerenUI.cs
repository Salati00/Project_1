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
            pnl_Supplies.Hide();
            pnl_CashRegister.Hide();
            pnl_RepRev.Hide();
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
            else if (panelName == "DrinkSup")
            {
                HideAll();

                pnl_Supplies.Show();

                //Txt_Supplies_NewName.Enabled = false;
                //Txt_Supplies_NewStock.Enabled = false;
                Btn_Supplies_Save.Enabled = false;
                Txt_Supplies_Id.Text = string.Empty;
                Txt_Supplies_NewName.Text = string.Empty;
                Txt_Supplies_NewStock.Text = string.Empty;
                Txt_Supplies_Price.Text = string.Empty;
                Txt_Supplies_Sold.Text = string.Empty;

                // fill the students listview within the students panel with a list of students
                Drink_Service drinkService = new Drink_Service();
                List<Drink> DrinkList = drinkService.GetDrinks();

                ListViewStockPrint(Lst_Supplies, DrinkList);

            }
            else if(panelName == "Activities")
            {
                HideAll();

                
            }
            else if(panelName == "CashRegister")
            {
                HideAll();
                pnl_CashRegister.Show();
                FillRegistryStudents();
                FillRegistryDrinks();
                Btn_Register_Checkout.Enabled = false;
            }
            else if (panelName == "RepRev")
            {
                HideAll();
                pnl_RepRev.Show();
                mcRev.MaxDate = DateTime.Today;
                PrintReport();
            }
        }

        public void FillRegistryStudents()
        {
            Student_Service studService = new Student_Service();
            List<Student> studentList = studService.GetStudents();

            Lst_RegStu.Items.Clear();
            foreach (Student s in studentList)
            {
                var row = new string[] { s.Number.ToString(), s.FullName };
                var lvi = new ListViewItem(row);
                Lst_RegStu.Items.Add(lvi);
            }
        }
        public void FillRegistryDrinks()
        {
            Register_Service regServ = new Register_Service();
            List<Drink> DrinkList = regServ.GetDrinks();

            Lst_RegDrink.Items.Clear();
            foreach (Drink d in DrinkList)
            {
                var row = new string[] { d.ID.ToString(), d.Name, d.Cost.ToString(), d.Stock.ToString() };
                var lvi = new ListViewItem(row);
                Lst_RegDrink.Items.Add(lvi);
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

        public void ListViewStockPrint(ListView lv, List<Drink> drinks)
        {
            lv.Items.Clear();
            foreach (Drink d in drinks)
            {
                var row = new string[] { d.ID.ToString(), d.Cost.ToString(), d.Name.ToString(), d.Stock.ToString(), d.Sold.ToString(), ((d.Stock < 10)? "Stock nearly depleted":"Stock Sufficient") };
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

        private void btn_Checkout_Click(object sender, EventArgs e)
        {
            Register_Service reg = new Register_Service();
            ListViewItem stu = Lst_RegStu.SelectedItems[0];


            foreach (ListViewItem item in Lst_RegDrink.CheckedItems)
            {
                reg.InsertSale(Convert.ToInt32(stu.SubItems[0].Text), Convert.ToInt32(item.SubItems[0].Text));
            }

            showPanel("CashRegister");
        }

        //Event fired for changed index selection in Supplies ListView
        private void Lst_Supplies_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lsv = (ListView)sender;

            if (lsv.SelectedItems.Count > 0)
            {
                ListViewItem item = lsv.SelectedItems[0];
                Txt_Supplies_Id.Text = item.SubItems[0].Text;
                Txt_Supplies_NewName.Text = item.SubItems[2].Text;
                Txt_Supplies_NewStock.Text = item.SubItems[3].Text;

                Txt_Supplies_NewName.Enabled = true;
                Txt_Supplies_NewStock.Enabled = true;
                Btn_Supplies_Save.Enabled = true;

                Txt_Supplies_Price.Visible = false;
                Txt_Supplies_Sold.Visible = false;
                Lbl_Supplies_Price.Visible = false;
                Lbl_Supplies_AmountSold.Visible = false;

                Btn_Supplies_Save.Text = "Save Changes";
            }
            else
            {
                Txt_Supplies_NewName.Text = string.Empty;
                Txt_Supplies_NewStock.Text = string.Empty;
                Txt_Supplies_Id.Text = string.Empty;
                Txt_Supplies_Price.Text = "";
                Txt_Supplies_Sold.Text = "";

                Txt_Supplies_Price.Visible = true;
                Txt_Supplies_Sold.Visible = true;
                Lbl_Supplies_Price.Visible = true;
                Lbl_Supplies_AmountSold.Visible = true;
                Btn_Supplies_Save.Text = "Add";
            }
        }

        //Event fired for button press in Supplies
        private void Btn_Supplies_Save_Click(object sender, EventArgs e)
        {
            Drink_Service sv = new Drink_Service();
            if (!Txt_Supplies_Price.Visible)
                sv.UpdateDrink(Convert.ToInt32(Txt_Supplies_Id.Text), Txt_Supplies_NewName.Text, Convert.ToInt32(Txt_Supplies_NewStock.Text));
            else
                sv.AddDrink(Txt_Supplies_NewName.Text, Convert.ToInt32(Txt_Supplies_NewStock.Text), Convert.ToInt32(Txt_Supplies_Sold.Text), Convert.ToInt32(Txt_Supplies_Price.Text));
            showPanel("DrinkSup");
        }

        private void Lst_RegStu_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnDisableRegistryButton(Lst_RegStu, Lst_RegDrink);
        }

        private void EnDisableRegistryButton(ListView Students, ListView Drinks)
        {
            bool enable = false;
            if (Students.SelectedItems.Count > 0)
            {
                enable = true;
            }
            else
            {
                enable = false;
            }

            if (Drinks.CheckedItems.Count > 0 && enable)
            {
                enable = true;
            }
            else
            {
                enable = false;
            }

            Btn_Register_Checkout.Enabled = enable;
        }

        private void Lst_RegDrink_ItemCheck(object sender, ItemCheckedEventArgs e)
        {
            EnDisableRegistryButton(Lst_RegStu, Lst_RegDrink);
        }

        private void cashRegister1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("CashRegister");
        }

        private void drinkSupplies1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("DrinkSup");
        }

        private void reportRevenueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("RepRev");

        }

        private void btn_RepRev_Calc_Click(object sender, EventArgs e)
        {
            PrintReport(mcRev.SelectionStart, mcRev.SelectionEnd);
        }

        private void PrintReport(DateTime from, DateTime until)
        {
            lvRepRev.Items.Clear();
            Report_Service service = new Report_Service();
            Report report = service.GetReport(from, until);
            var row = new string[] { report.Sales.ToString(), report.Turnover.ToString(), report.CustomerID.ToString() };
            var lvi = new ListViewItem(row);
            lvRepRev.Items.Add(lvi);
        }
        private void PrintReport()
        {
            lvRepRev.Items.Clear();
            Report_Service service = new Report_Service();
            Report report = service.GetReport();
            var row = new string[] { report.Sales.ToString(), report.Turnover.ToString(), report.CustomerID.ToString() };
            var lvi = new ListViewItem(row);
            lvRepRev.Items.Add(lvi);
        }

        private void Txt_Supplies_TextChanged(object sender, EventArgs e)
        {
            if(Txt_Supplies_NewName.Text != "" && Txt_Supplies_NewStock.Text != "" && Txt_Supplies_Price.Text != "" && Txt_Supplies_Sold.Text != "")
            {
                Btn_Supplies_Save.Enabled = true;
            }
            else
            {
                Btn_Supplies_Save.Enabled = false;
            }
        }
    }
}
