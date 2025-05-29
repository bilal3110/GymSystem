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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace GymManagementSystem
{
    public partial class editEquipments : Form
    {
        public string equipmentId { get; set; }
        public editEquipments(string id)
        {
            InitializeComponent();
            equipmentId = id;
        }
        public editEquipments(string id, string eqName, string eqMuscle, string eqPrice,
            string eqQuantity)
        {
            InitializeComponent();
            equipmentId = id;
            equipment.Text = eqName;
            muscle.Text = eqMuscle;
            price.Text = eqPrice;
            quantity.Text = eqQuantity;
        }
        public editEquipments()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            UPDATE equipments
            SET equipment = @equipment,
                quantity = @quantity,
                price = @price,
                muscle = @muscle
            WHERE e_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", equipmentId);
                    cmd.Parameters.AddWithValue("@equipment", equipment.Text.Trim());

                    string qtyText = quantity.Text.Trim();
                    string priceText = price.Text.Trim();

                    if (long.TryParse(qtyText, out long qty) && long.TryParse(priceText, out long prc))
                    {
                        cmd.Parameters.AddWithValue("@quantity", qty);
                        cmd.Parameters.AddWithValue("@price", prc);
                    }
                    else
                    {
                        MessageBox.Show("Quantity or Price is not a valid number.\n" +
                                        $"Quantity: '{qtyText}' | Price: '{priceText}'");
                        return;
                    }

                    cmd.Parameters.AddWithValue("@muscle", muscle.Text.Trim());


                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            MessageBox.Show("Equipment updated successfully!");
                        else
                            MessageBox.Show("No equipment found with that ID.");
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
            equipment.Text = "";
            muscle.Text = "";
            price.Text = "";
            quantity.Text = "";

            equipment.Focus();
        }

        private void label5_Click(object sender, EventArgs e)
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

        private void price_TextChanged(object sender, EventArgs e)
        {

        }

        private void equipment_TextChanged(object sender, EventArgs e)
        {

        }

        private void muscle_TextChanged(object sender, EventArgs e)
        {

        }

        private void quantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
