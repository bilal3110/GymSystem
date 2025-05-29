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
    public partial class viewSupplements : Form
    {
        public viewSupplements()
        {
            InitializeComponent();

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;

            LoadSupplementsGrid();
        }

        private void LoadSupplementsGrid()
        {
            button3_Click(null, null);
        }


        private void AddButtonsToGrid()
        {
            if (dataGridView1.Columns.Contains("Edit") || dataGridView1.Columns.Contains("Delete"))
                return;

            DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
            editButton.HeaderText = "Edit";
            editButton.Text = "Edit";
            editButton.Name = "Edit";
            editButton.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editButton);

            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.HeaderText = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.Name = "Delete";
            deleteButton.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteButton);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.BackColor = Color.LightGreen;
                cell.Style.ForeColor = Color.Black;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.BackColor = Color.IndianRed;
                cell.Style.ForeColor = Color.White;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                string id = row.Cells["s_id"].Value.ToString();
                string name = row.Cells["s_name"].Value.ToString();
                string type = row.Cells["s_type"].Value.ToString();
                string price = row.Cells["price"].Value.ToString();
                string quantity = row.Cells["quantity"].Value.ToString();


                var editForm = new editSuplementscs(
                    id, name, type, price, quantity
                );

                editForm.FormClosed += (s, args) => LoadSupplementsGrid();

                editForm.ShowDialog();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["s_id"].Value != null)
                {
                    try
                    {
                        int supId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["s_id"].Value);
                        DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            DeleteSupplements(supId);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving staff ID: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Staff ID is missing.");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            string query = "SELECT * FROM Supplements";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();

                try
                {
                    conn.Open();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    AddButtonsToGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void DeleteSupplements(int supId)
        {
            string connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            string query = "DELETE FROM Supplements WHERE s_id = @supID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@supID", supId);
                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplement deleted successfully.");
                        button3_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No Supplement found with the specified ID.");
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String searchtxt = searchBox.Text;
            String connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            string query = @"
                            SELECT * FROM Supplements
                            WHERE 
                            s_name LIKE @searchText OR
                            s_type LIKE @searchText OR
                            quantity LIKE @searchText OR
                            price LIKE @searchText";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@searchText", "%" + searchtxt + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            searchBox.Text = "";
        }
    }
}
