using System;

using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data;

namespace Employee1_app
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=SMARTMETER1\\SQLEXPRESS;Initial Catalog=Emp;Integrated Security=True;");

        public Form1()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mini_btn_Click(object sender, EventArgs e)
        {
        
            this.WindowState = FormWindowState.Minimized;

        }

        private void Sign_Up_Click(object sender, EventArgs e)
        { 
                Form2 form2 = new Form2();
                form2.Show(); 
               
            

        }

        private void Login_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(Username.Text) && !string.IsNullOrEmpty(Password.Text))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Employee WHERE Username = @Username AND Password = @Password", connection);
                    cmd.Parameters.AddWithValue("@Username", Username_box.Text);
                    cmd.Parameters.AddWithValue("@Password", password_box.Text);

                    int count = (int)cmd.ExecuteScalar();
                    connection.Close();

                    if (count > 0)
                    {
                        MessageBox.Show("Login successful!");



                        Form3 form3 = new Form3();

                        form3.Show();

                        this.Hide();

                    }



                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter both username and password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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
    }
    }

