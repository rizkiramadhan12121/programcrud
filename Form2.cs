using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProgramCRUD
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string EmployeeNo
        {
            get { return txtEmpNo.Text; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLname.Text) ||
                string.IsNullOrWhiteSpace(txtfname.Text) ||
                string.IsNullOrWhiteSpace(txtMname.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("All fields are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=Rizki-RPL\\SQLEXPRESS;Initial Catalog=ProgramCrud;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO tblemployee (lname, fname, mname, bdate, gender, address, contact) " +
                                   "VALUES (@LastName, @FirstName, @MiddleName, @BirthDate, @Gender, @Address, @Contact)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar).Value = txtLname.Text;
                        cmd.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar).Value = txtfname.Text;
                        cmd.Parameters.Add("@MiddleName", System.Data.SqlDbType.NVarChar).Value = txtMname.Text;
                        cmd.Parameters.Add("@BirthDate", System.Data.SqlDbType.Date).Value = dtBdate.Value.Date;
                        cmd.Parameters.Add("@Gender", System.Data.SqlDbType.NVarChar).Value = cboGender.SelectedItem.ToString();
                        cmd.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = txtAddress.Text;
                        cmd.Parameters.Add("@Contact", System.Data.SqlDbType.NVarChar).Value = txtContact.Text;

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmpNo.Text) ||
                string.IsNullOrWhiteSpace(txtLname.Text) ||
                string.IsNullOrWhiteSpace(txtfname.Text) ||
                string.IsNullOrWhiteSpace(txtMname.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("All fields are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=Rizki-RPL\\SQLEXPRESS;Initial Catalog=ProgramCrud;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE tblemployee SET lname = @LastName, fname = @FirstName, mname = @MiddleName, " +
                                   "bdate = @BirthDate, gender = @Gender, address = @Address, contact = @Contact " +
                                   "WHERE employeeno = @EmployeeNo";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeNo", txtEmpNo.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLname.Text);
                        cmd.Parameters.AddWithValue("@FirstName", txtfname.Text);
                        cmd.Parameters.AddWithValue("@MiddleName", txtMname.Text);
                        cmd.Parameters.AddWithValue("@BirthDate", dtBdate.Value.Date);
                        cmd.Parameters.AddWithValue("@Gender", cboGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Contact", txtContact.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No employee found with the given Employee Number.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public string LastName
        {
            get => txtLname.Text;
            set => txtLname.Text = value;
        }

        public string FirstName
        {
            get => txtfname.Text;
            set => txtfname.Text = value;
        }

        public string MiddleName
        {
            get => txtMname.Text;
            set => txtMname.Text = value;
        }

        public DateTime BirthDate
        {
            get => dtBdate.Value;
            set => dtBdate.Value = value;
        }

        public string Gender
        {
            get => cboGender.SelectedItem?.ToString();
            set => cboGender.SelectedItem = value;
        }

        public string Address
        {
            get => txtAddress.Text;
            set => txtAddress.Text = value;
        }

        public string Contact
        {
            get => txtContact.Text;
            set => txtContact.Text = value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
