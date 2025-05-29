using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class EditStaff : Form
    {
        public string staffId { get; set; }
        public EditStaff(string id)
        {
            InitializeComponent();
            staffId = id;

        }
        public EditStaff(string id, string sfname, string slname, string sgender,
                 string mobile, string semail, string spay, string sjoinDate, string address)
        {
            InitializeComponent();
            staffId = id;
            fname.Text = sfname;
            lname.Text = slname;
            gender.Text = sgender;
            phone.Text = mobile;
            email.Text = semail;
            pay.Text = spay;
            if (DateTime.TryParse(sjoinDate, out DateTime validDate))
            {
                joinDate.Value = validDate;
            }
            else
            {
                MessageBox.Show("Are You Sure!!");
            }
            add.Text = address;
        }

        public EditStaff()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            UPDATE staff
               SET Fname    = @fname,
                   Lname    = @lname,
                   Gender   = @gender,
                   Mobile   = @mobile,
                   Email    = @email,
                   Pay      = @pay,
                   JoinDate = @joinDate,
                   SAddress = @address
             WHERE s_id = @id";           

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fname", fname.Text);
                    cmd.Parameters.AddWithValue("@lname", lname.Text);
                    cmd.Parameters.AddWithValue("@gender", gender.Text);
                    cmd.Parameters.AddWithValue("@mobile", phone.Text);
                    cmd.Parameters.AddWithValue("@email", email.Text);
                    cmd.Parameters.AddWithValue("@joinDate", DateTime.Parse(joinDate.Text));
                    cmd.Parameters.AddWithValue("@pay", pay.Text);
                    cmd.Parameters.AddWithValue("@address", add.Text);
                    cmd.Parameters.AddWithValue("@id", staffId);


                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Staff updated successfully!");
                    else
                        MessageBox.Show("No staff found with that ID.");
                }
            }
        }
    }
}
