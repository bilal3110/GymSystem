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
    public partial class Equipments : Form
    {
        public Equipments()
        {
            InitializeComponent();
        }

        private void add_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Description_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void gender_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void mebershipTime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gymTime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DOB_ValueChanged(object sender, EventArgs e)
        {

        }

        private void joinDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }

        private void fname_TextChanged(object sender, EventArgs e)
        {

        }

        private void lname_TextChanged(object sender, EventArgs e)
        {

        }

        private void phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string equipName = equipment.Text;
            long qty = Int64.Parse(quantity.Text);
            long priceValue = Int64.Parse(price.Text);      
            string muscleGroup = muscle.Text;   

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";

            try
            {
                string query = "INSERT INTO equipments (equipment, quantity, price, muscle) " +
                               "VALUES (@equipment, @quantity, @price, @muscle)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@equipment", equipName);
                cmd.Parameters.AddWithValue("@quantity", qty);
                cmd.Parameters.AddWithValue("@price", priceValue);
                cmd.Parameters.AddWithValue("@muscle", muscleGroup);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Equipment inserted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            equipment.Text = "";
            quantity.Text = "";
            price.Text = "";
            muscle.Text = "";

            equipment.Focus();
        }
    }
}
