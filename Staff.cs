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
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fname.Text = "";
            lname.Text = "";
            phone.Text = "";
            email.Text = "";
            pay.Text = "";
            add.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            fname.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Fname = fname.Text;
            String Lname = lname.Text;
            String Phone = phone.Text;
            String Email = email.Text;
            String Pay = pay.Text;
            String Address = add.Text;
            String JoinDate = joinDate.Text;

            String gender = radioButton2.Checked ? radioButton2.Text : radioButton1.Text;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";

            try
            {
                String query = "INSERT INTO staff (Fname,Lname,Gender,Mobile,Email,Pay,JoinDate,SAddress) " +
                               "VALUES(@Fname,@Lname,@Gender,@Mobile,@Email,@Pay,@JoinDate,@SAddress)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("Fname", Fname);
                cmd.Parameters.AddWithValue("Lname", Lname);
                cmd.Parameters.AddWithValue("Gender", gender);
                cmd.Parameters.AddWithValue ("Mobile",Phone);
                cmd.Parameters.AddWithValue("Email", Email);
                cmd.Parameters.AddWithValue("Pay", Pay);
                cmd.Parameters.AddWithValue("JoinDate", JoinDate);
                cmd.Parameters.AddWithValue("SAddress", Address);

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
    }
}
