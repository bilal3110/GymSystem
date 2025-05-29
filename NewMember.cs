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
    public partial class NewMember : Form
    {
        public NewMember()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String firstName = fname.Text;
            String lastName = lname.Text;
            String phoneNumber = phone.Text;
            String emaill = email.Text;
            String address = add.Text;

            String gender = radioButton2.Checked ? radioButton2.Text : radioButton1.Text;

            String dob = DOB.Text;
            String JoinDate = joinDate.Text;
            String mTime = mebershipTime.Text;
            String gTime = gymTime.Text;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";

            try
            {
                string query = "INSERT INTO member (Fname, Lname, Gender, Dob, Mobile, Email, JoinDate, Gymtime, Maddress, MembershipTime) " +
                               "VALUES (@Fname, @Lname, @Gender, @Dob, @Mobile, @Email, @JoinDate, @Gymtime, @Maddress, @MembershipTime)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Fname", firstName);
                cmd.Parameters.AddWithValue("@Lname", lastName);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Dob", DateTime.Parse(dob));
                cmd.Parameters.AddWithValue("@Mobile", phoneNumber);
                cmd.Parameters.AddWithValue("@Email", emaill);
                cmd.Parameters.AddWithValue("@JoinDate", DateTime.Parse(JoinDate));
                cmd.Parameters.AddWithValue("@Gymtime", gTime);
                cmd.Parameters.AddWithValue("@Maddress", address);
                cmd.Parameters.AddWithValue("@MembershipTime", mTime);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Member data inserted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fname.Text = "";
            lname.Text = "";
            phone.Text = "";
            email.Text = "";
            add.Text = "";
            DOB.Text = "";
            joinDate.Text = "";
            mebershipTime.Text = "";
            gymTime.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;

            fname.Focus();
        }

    }
}
