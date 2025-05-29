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
    public partial class EditMemberForm : Form
    {
        public string MemberId { get; set; }

        public EditMemberForm(string id)
        {
            InitializeComponent();
            MemberId = id;
        }
        public EditMemberForm(string id, string fname, string lname, string gender, string dob,
                              string mobile, string email, string joinDate, string gymTime,
                              string address, string membershipTime)
        {
            InitializeComponent();

            MemberId = id; 
            txtFname.Text = fname;
            txtLname.Text = lname;
            txtGender.Text = gender;
            txtDob.Text = dob;
            txtMobile.Text = mobile;
            txtEmail.Text = email;
            txtJoinDate.Text = joinDate;
            txtGymtime.Text = gymTime;
            txtAddress.Text = address;
            txtMembershipTime.Text = membershipTime;
        }
        public EditMemberForm()
        {
            InitializeComponent();
        }

        private void txtMembershipTime_Load(object sender, EventArgs e)
        {

        }


        private void EditMemberForm_Load(object sender, EventArgs e)
        {
            string connStr = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM member WHERE m_id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", MemberId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtFname.Text = reader["Fname"].ToString();
                    txtLname.Text = reader["Lname"].ToString();
                    txtGender.Text = reader["Gender"].ToString();
                    txtDob.Text = reader["Dob"].ToString();
                    txtMobile.Text = reader["Mobile"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtJoinDate.Text = reader["JoinDate"].ToString();
                    txtGymtime.Text = reader["Gymtime"].ToString();
                    txtAddress.Text = reader["Maddress"].ToString();
                    txtMembershipTime.Text = reader["MembershipTime"].ToString();
                }
            }
        }


        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            string connStr = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"UPDATE member SET 
                                Fname=@fname, Lname=@lname, Gender=@gender, Dob=@dob,
                                Mobile=@mobile, Email=@email, JoinDate=@joinDate,
                                Gymtime=@gymTime, Maddress=@address, MembershipTime=@membership
                             WHERE m_id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fname", txtFname.Text);
                cmd.Parameters.AddWithValue("@lname", txtLname.Text);
                cmd.Parameters.AddWithValue("@gender", txtGender.Text);
                cmd.Parameters.AddWithValue("@dob", DateTime.Parse(txtDob.Text));
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@joinDate", DateTime.Parse(txtJoinDate.Text));
                cmd.Parameters.AddWithValue("@gymTime", txtGymtime.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@membership", txtMembershipTime.Text);
                cmd.Parameters.AddWithValue("@id", MemberId);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Member updated successfully!");
                this.Close();
            }
        }
    }
}
