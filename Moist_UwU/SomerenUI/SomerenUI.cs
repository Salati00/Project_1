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

        private void showPanel(string panelName)
        {
            switch (panelName)
            {
                case "Students":
                    Student_Service studService = new Student_Service();
                    List<Student> studentList = studService.GetStudents();

                    ListViewStuPrint(listViewStudents, studentList);
                    break;

                case "Lecturers":
                    Lecturer_Service lecService = new Lecturer_Service();
                    List<Teacher> lecList = lecService.GetTeachers();

                    ListViewLecPrint(listView_Lec, lecList);
                    break;

                case "Supervisors":
                    Supervisor_Service supService = new Supervisor_Service();
                    List<Supervisor> supList = supService.GetSupervisors();
                    PrintSup(listView_Sup, supList);
                    Activity_Service actService = new Activity_Service();
                    List<Activity> actList = actService.GetActivities();
                    cmb_ActivityID.DataSource = actList;
                    cmb_ActivityID.DisplayMember = "Name";
                    cmb_ActivityID.ValueMember = "ID";
                    break;

                case "Rooms":
                    Room_Service roomService = new Room_Service();
                    List<Room> roomList = roomService.GetRooms();

                    ListViewRoomPrint(listViewRooms, roomList);
                    break;

                case "DrinkSup":
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
                    break;

                case "Activities":
                    Dtp_Activities_TimePart.Format = DateTimePickerFormat.Custom;
                    Dtp_Activities_TimePart.CustomFormat = "hh:mm tt";
                    Dtp_Activities_TimePart.ShowUpDown = true;

                    Txt_Activities_Id.Text = "";
                    Txt_Activities_Description.Text = "";
                    Txt_Activities_Location.Text = "";
                    Txt_Activities_Name.Text = "";
                    Dtp_Activities_DatePart.Value = DateTime.Today;
                    Dtp_Activities_TimePart.Value = DateTime.UtcNow;

                    Activity_Service actServe = new Activity_Service();
                    List<Activity> ActivityList = actServe.GetActivities();

                    ListViewActivitiesPrint(Lst_Activities, ActivityList);
                    break;

                case "CashRegister":
                    FillRegistryStudents();
                    FillRegistryDrinks();
                    Btn_Register_Checkout.Enabled = false;
                    break;

                case "RevRep":
                    mcRev.MaxDate = DateTime.Today;
                    PrintReport();
                    break;

                default:
                    break;
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

        public void ListViewActivitiesPrint(ListView lv, List<Activity> activities)
        {
            lv.Items.Clear();
            foreach (Activity a in activities)
            {
                var row = new string[] { a.ID.ToString(), a.Name, a.Location.ToString(), a.Date.ToString(), a.Description.ToString() };
                var lvi = new ListViewItem(row);
                lv.Items.Add(lvi);
            }

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
        private void PrintSup(ListView lv, List<Supervisor> supList)
        {
            lv.Items.Clear();
            foreach (Supervisor s in supList)
            {
                var row = new string[] { s.SuperviseID.ToString(), s.Name, s.ActivityName.ToString() };
                var lvi = new ListViewItem(row);
                lv.Items.Add(lvi);
            }
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

        private void TabControl_Main_SelectedIndexChanged(object sender, EventArgs e)//Rese
        {
            switch (TabControl_Main.SelectedIndex)
            {
                case 0:
                    { /**/ }
                    break;
                case 1:
                    { showPanel("Students"); }
                    break;
                case 2:
                    { showPanel("Lecturers"); }
                    break;
                case 3:
                    { showPanel("Supervisors"); }
                    break;
                case 4:
                    { showPanel("Activities"); }
                    break;
                case 5:
                    { showPanel("Rooms"); }
                    break;
                case 6:
                    {
                        switch (TabControl_Bar.SelectedIndex)
                        {
                            case 0:
                                showPanel("CashRegister");
                                break;
                            case 1:
                                showPanel("DrinkSup");
                                break;
                            case 2:
                                showPanel("RevRep");
                                break;

                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren kids!");
        }

        /*private void Lst_Activities_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }*/

        private void EnDisableActivityButtons(bool Override = false)
        {
            if (Lst_Activities.SelectedItems.Count > 0 && !Override)
            {
                Btn_Activities_Add.Text = "Save Changes";
                Btn_Activities_Delete.Visible = true;

                ListViewItem item = Lst_Activities.SelectedItems[0];
                Txt_Activities_Id.Text = item.SubItems[0].Text;
                Txt_Activities_Name.Text = item.SubItems[1].Text;
                Txt_Activities_Location.Text = item.SubItems[2].Text;
                Txt_Activities_Description.Text = item.SubItems[4].Text;
                Dtp_Activities_DatePart.Value = Convert.ToDateTime(item.SubItems[3].Text);
                Dtp_Activities_TimePart.Value = Convert.ToDateTime(item.SubItems[3].Text);
            }
            else
            {
                Btn_Activities_Add.Text = "Add";
                Btn_Activities_Delete.Visible = false;

                showPanel("Activities");
            }

                
        }

        private void SomerenUI_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Activities_Delete_Click(object sender, EventArgs e)
        {
            Activity_Service eys = new Activity_Service();
            if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                eys.RemoveActivity(Convert.ToInt32(Txt_Activities_Id.Text));
                EnDisableActivityButtons(Override: true);
            }
        }

        private void Btn_Activities_Add_Click(object sender, EventArgs e)
        {
            Activity_Service eys = new Activity_Service();
            if (!Btn_Activities_Delete.Visible)
            {
                try
                {
                    eys.InsertActivity(Txt_Activities_Name.Text, Txt_Activities_Location.Text, (Dtp_Activities_DatePart.Value.Date + Dtp_Activities_TimePart.Value.TimeOfDay), Txt_Activities_Description.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    eys.UpdateActivity(Convert.ToInt32(Txt_Activities_Id.Text), Txt_Activities_Name.Text, Txt_Activities_Location.Text, (Dtp_Activities_DatePart.Value.Date + Dtp_Activities_TimePart.Value.TimeOfDay), Txt_Activities_Description.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Btn_Activities_Add.Text = "Add";
            Btn_Activities_Delete.Visible = false;

            showPanel("Activities");
        }

        private void btn_RemoveSup_Click(object sender, EventArgs e)
        {
            Supervisor_Service service = new Supervisor_Service();
            if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                service.RemoveSup(Convert.ToInt32(listView_Sup.SelectedItems[0].Text));
                foreach (ListViewItem item in listView_Sup.SelectedItems)
                {
                    listView_Sup.Items.Remove(item);
                }
            }
        }

        private void Lst_Activities_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            EnDisableActivityButtons();
        }

        private void btn_AddSupp_Click(object sender, EventArgs e)
        {
            if (txtBox_Sup.Text != "" && Convert.ToInt32(txtBox_Sup.Text) > 0)
            {
                Supervisor_Service supServiceAdd = new Supervisor_Service();
                try
                {
                    supServiceAdd.InsertSupervisor(Convert.ToInt32(txtBox_Sup.Text), Convert.ToInt32(cmb_ActivityID.SelectedValue));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid Entry");
                }
                showPanel("Supervisors");
            }
            else
                MessageBox.Show("Please enter a teacher ID");
        }
    }
}
