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
    public partial class viewStaff : Form
    {
        public viewStaff()
        {
            InitializeComponent();
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            cmbGender.SelectedIndex = 0;
        }

        private void LoadStaffGrid()
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
                string fname = row.Cells["Fname"].Value.ToString();
                string lname = row.Cells["Lname"].Value.ToString();
                string gender = row.Cells["Gender"].Value.ToString();
                string mobile = row.Cells["Mobile"].Value.ToString();
                string email = row.Cells["Email"].Value.ToString();
                string pay = row.Cells["Pay"].Value.ToString();
                string joinDate = Convert.ToDateTime(row.Cells["JoinDate"].Value)
                                                   .ToString("yyyy-MM-dd");
                string address = row.Cells["SAddress"].Value.ToString();  

                var editForm = new EditStaff(
                    id, fname, lname, gender, joinDate,  
                    mobile, email, pay, address
                );

                editForm.FormClosed += (s, args) => LoadStaffGrid();

                editForm.ShowDialog();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["s_id"].Value != null)
                {
                    try
                    {
                        int staffId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["s_id"].Value);
                        DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            DeleteStaff(staffId);
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
        
        
        private void btnFilter_Click(object sender, EventArgs e)
        {
            string connStr = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM staff WHERE 1=1";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (cmbGender.SelectedItem != null && cmbGender.SelectedItem.ToString() != "All")
                {
                    query += " AND Gender = @gender";
                    parameters.Add(new SqlParameter("@gender", cmbGender.SelectedItem.ToString()));
                }

                DateTime fromDate = dtpJoinDateFrom.Value.Date;
                DateTime toDate = dtpJoinDateTo.Value.Date.AddDays(1).AddSeconds(-1);

                if (fromDate <= toDate)
                {
                    query += " AND JoinDate BETWEEN @fromDate AND @toDate";
                    parameters.Add(new SqlParameter("@fromDate", fromDate));
                    parameters.Add(new SqlParameter("@toDate", toDate));
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddRange(parameters.ToArray());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
        }

        private void DeleteStaff(int staffId)
        {
            string connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            string query = "DELETE FROM staff WHERE s_id = @StaffID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StaffID", staffId);
                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Staff member deleted successfully.");
                        button3_Click(null, null); 
                    }
                    else
                    {
                        MessageBox.Show("No staff member found with the specified ID.");
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

        
        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            string query = "SELECT * FROM staff";

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

        private void viewStaff_Load(object sender, EventArgs e)
        {
            button3_Click(null, null); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String searchtxt = searchBox.Text;
            String connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            string query = @"
                            SELECT * FROM staff
                            WHERE 
                            Fname LIKE @searchText OR
                            Lname LIKE @searchText OR
                            Mobile LIKE @searchText OR
                            Email LIKE @searchText OR
                            Pay LIKE @searchText OR
                            Saddress LIKE @searchText";
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
        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dtpJoinDateTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpJoinDateFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}