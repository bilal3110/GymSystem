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
    public partial class viewMembers : Form
    {
        public viewMembers()
        {
            InitializeComponent();
            cmbGender.SelectedIndex = 0;
            cmbMembershipTime.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            string query = "SELECT * FROM member";

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName == "Edit")
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                string id = row.Cells["m_id"].Value.ToString();
                string fname = row.Cells["Fname"].Value.ToString();
                string lname = row.Cells["Lname"].Value.ToString();
                string gender = row.Cells["Gender"].Value.ToString();
                string dob = Convert.ToDateTime(row.Cells["Dob"].Value).ToString("yyyy-MM-dd");
                string mobile = row.Cells["Mobile"].Value.ToString();
                string email = row.Cells["Email"].Value.ToString();
                string joinDate = Convert.ToDateTime(row.Cells["JoinDate"].Value).ToString("yyyy-MM-dd");
                string gymTime = row.Cells["Gymtime"].Value.ToString();
                string address = row.Cells["Maddress"].Value.ToString();
                string membershipTime = row.Cells["MembershipTime"].Value.ToString();

                EditMemberForm editForm = new EditMemberForm(id, fname, lname, gender, dob,
                                                             mobile, email, joinDate, gymTime,
                                                             address, membershipTime);
                editForm.ShowDialog();
            }
            else if (columnName == "Delete")
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells["m_id"].Value.ToString();
                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DeleteMember(id);
                    button3_Click(null, null);
                }
            }
        }

        private void DeleteMember(string id)
        {
            try
            {
                if (!int.TryParse(id, out int memberId))
                {
                    MessageBox.Show("Invalid ID format.", "Error");
                    return;
                }

                string connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
                string query = "DELETE FROM member WHERE m_id = @id";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = memberId;
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            MessageBox.Show($"No record found with ID: {id}", "Warning");
                        }
                        else
                        {
                            MessageBox.Show("Record deleted successfully", "Success");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error");
            }
        }

        private void viewMembers_Load(object sender, EventArgs e)
        {
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
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

        private void button1_Click(object sender, EventArgs e)
        {
            String searchText = searchBox.Text;

            String connectionString = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";

            string query = @"
                            SELECT * FROM member 
                            WHERE 
                            Fname LIKE @searchText OR
                            Lname LIKE @searchText OR
                            Mobile LIKE @searchText OR
                            Email LIKE @searchText OR
                            Gymtime LIKE @searchText OR
                            Maddress LIKE @searchText OR
                            MembershipTime LIKE @searchText";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string connStr = "data source=DESKTOP-O2789LR; database=gym; integrated security=true";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM member WHERE 1=1";
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

                if (cmbMembershipTime.SelectedItem != null && cmbMembershipTime.SelectedItem.ToString() != "All")
                {
                    query += " AND MembershipTime = @membership";
                    parameters.Add(new SqlParameter("@membership", cmbMembershipTime.SelectedItem.ToString()));
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddRange(parameters.ToArray());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;

                MessageBox.Show("Results: " + dt.Rows.Count);
            }
        }

    }
}
