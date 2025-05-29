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
    public partial class addSuplement : Form
    {
        public addSuplement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = supName.Text;
            string type = supType.Text;
            string quantity = supQuantity.Text;
            long price;

            if (!long.TryParse(supPrice.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            string connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            string query = "INSERT INTO Supplements (s_name, s_type, quantity, price) " +
                           "VALUES (@name, @type, @quantity, @price)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@price", price);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Supplement added successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            supName.Text = "";
            supPrice.Text = "";
            supQuantity.Text = "";
            supType.Text = "";

            supName.Focus();
        }
    }
}
