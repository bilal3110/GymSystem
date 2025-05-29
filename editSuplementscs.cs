using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class editSuplementscs : Form
    {
        public string supllementId { get; set; }
        public editSuplementscs(string id)
        {
            InitializeComponent();
            supllementId = id;
        }
        public editSuplementscs(string id, string supllementName, string supllementType, string supllementPrice,
            string supllementQuantity)
        {
            InitializeComponent();
            supllementId = id;
            supName.Text = supllementName;
            supType.Text = supllementType;
            supPrice.Text = supllementPrice;
            supQuantity.Text = supllementQuantity;
        }
        public editSuplementscs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            UPDATE Supplements
            SET s_name = @name,
                s_type = @type,
                quantity = @quantity,
                price = @price
            WHERE s_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", supllementId);
                    cmd.Parameters.AddWithValue("@name", supName.Text.Trim());
                    cmd.Parameters.AddWithValue("@type", supType.Text.Trim());
                    cmd.Parameters.AddWithValue("@quantity", supQuantity.Text.Trim());

                    string priceText = supPrice.Text.Trim();

                    if (long.TryParse(priceText, out long prc))
                    {
                        cmd.Parameters.AddWithValue("@price", prc);
                    }
                    else
                    {
                        MessageBox.Show("Price is not a valid number.\n" +
                                        $"Price: '{priceText}'");
                        return;
                    }

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            MessageBox.Show("Supplement updated successfully!");
                        else
                            MessageBox.Show("No Supplement found with that ID.");
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
            supType.Text = "";
            supPrice.Text = "";
            supQuantity.Text = "";

            supName.Focus();
        }
    }
}
