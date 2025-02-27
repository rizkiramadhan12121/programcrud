using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProgramCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData(); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void LoadData(string searchKeyword = "")
        {
            string connectionString = "Data Source=Rizki-RPL\\SQLEXPRESS;Initial Catalog=ProgramCrud;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT employeeno AS [Employee ID], lname AS [Last Name], fname AS [First Name], " +
                                   "mname AS [Middle Name], bdate AS [Birthdate], gender AS [Gender], " +
                                   "address AS [Address], contact AS [Contact] FROM tblemployee";

                    if (!string.IsNullOrWhiteSpace(searchKeyword))
                    {
                        query += " WHERE lname LIKE @Search OR fname LIKE @Search OR mname LIKE @Search";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchKeyword))
                        {
                            cmd.Parameters.AddWithValue("@Search", "%" + searchKeyword + "%");
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;

                            ConfigureDataGridView();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadData()
        {
            string connectionString = "Data Source=Rizki-RPL\\SQLEXPRESS;Initial Catalog=ProgramCrud;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT employeeno AS [Employee ID], lname AS [Last Name], fname AS [First Name], " +
                                   "mname AS [Middle Name], bdate AS [Birthdate], gender AS [Gender], " +
                                   "address AS [Address], contact AS [Contact] FROM tblemployee";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                        ConfigureDataGridView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData(); 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string employeeNo = selectedRow.Cells["Employee ID"].Value.ToString();

            string connectionString = "Data Source=Rizki-RPL\\SQLEXPRESS;Initial Catalog=ProgramCrud;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM tblemployee WHERE employeeno = @employeeno";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@employeeno", employeeNo);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee deleted successfully!");
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("No employee found with the given ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            Form2 form2 = new Form2
            {
               
                LastName = selectedRow.Cells["Last Name"].Value.ToString(),
                FirstName = selectedRow.Cells["First Name"].Value.ToString(),
                MiddleName = selectedRow.Cells["Middle Name"].Value.ToString(),
                BirthDate = Convert.ToDateTime(selectedRow.Cells["Birthdate"].Value),
                Gender = selectedRow.Cells["Gender"].Value.ToString(),
                Address = selectedRow.Cells["Address"].Value.ToString(),
                Contact = selectedRow.Cells["Contact"].Value.ToString()
            };

            if (form2.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            LoadData(keyword);
        }
    }
}
