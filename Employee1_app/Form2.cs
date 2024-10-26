using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Employee1_app
{
    public partial class Form2 : Form
    {


        public Form2()
        {
            InitializeComponent();


        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        SqlConnection connection = new SqlConnection("Data Source=SMARTMETER1\\SQLEXPRESS;Initial Catalog=Emp;Integrated Security=True;Encrypt=False;");
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mini_btn_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void Create_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(Username.Text) && !string.IsNullOrEmpty(Email.Text) && !string.IsNullOrEmpty(Password.Text) && !string.IsNullOrEmpty(Con_pass.Text) && !string.IsNullOrEmpty(Address.Text))
                {
                   
                    if (Password.Text == Con_pass.Text)

                    {
                        int v = check(Email.Text);
                        if (v == 0) 
                        {
                            connection.Open();
                            
                            SqlCommand cmd = new SqlCommand("INSERT INTO Employee (Username, Email, Password,Address) VALUES (@Username, @Email, @Password, @Address)", connection);
                            cmd.Parameters.AddWithValue("@Username", Username.Text);
                            cmd.Parameters.AddWithValue("@Email", Email.Text);
                            cmd.Parameters.AddWithValue("@Password", Password.Text);
                            cmd.Parameters.AddWithValue("@Address", Address.Text);
                            cmd.ExecuteNonQuery();
                            connection.Close();

                            MessageBox.Show("SignUp Successfully");
                            Username.Text = "";
                            Email.Text = "";
                            Password.Text = "";
                            Con_pass.Text = "";
                            Address.Text = "";
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Hey, you are already Registered");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Passwords do not match");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill in all fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    LoadData();
                }
            }

            int check(string text)
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Employee WHERE Email ='" + Email + "'";
                SqlCommand cmd = new SqlCommand(query, connection);
                int v = (int)cmd.ExecuteScalar();
                connection.Close();
                return v;
            }






        }


        private void LoadData()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt); 
                dataGridView1.DataSource = dt; 
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            {

                foreach (Control control in this.Controls)
                {
                    if (control is TextBox)
                    {

                        ((TextBox)control).Clear();
                    }
                    else if (control is ComboBox)
                    {

                        ((ComboBox)control).SelectedIndex = -1;
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(Username.Text))


                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE Username = @Username", connection)
                    {

                    };
                    cmd.Parameters.AddWithValue("@Username", Username.Text);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Employee ID.");
                    }


                    Username.Text = "";


                    connection.Close();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Please enter the Username to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }


        }
    }

}




