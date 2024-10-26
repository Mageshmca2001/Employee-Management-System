using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using System.Xml.Linq;

namespace Employee1_app
{
    public partial class Form3 : Form
    {


        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=SMARTMETER1\\SQLEXPRESS;Initial Catalog=Emp2;Integrated Security=True;");
        private void Form3_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mini_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Emp_Name.Text) &&
                    !string.IsNullOrEmpty(Emp_id.Text) &&
                    !string.IsNullOrEmpty(Emp_address.Text) &&
                    !string.IsNullOrEmpty(Emp_phone.Text) &&
                    !string.IsNullOrEmpty(Emp_date.Text) &&
                    !string.IsNullOrEmpty(Emp_salary.Text))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Employee2 (Emp_Name, Emp_id, Emp_address, Emp_phone, Emp_date, Emp_salary) VALUES (@Emp_Name, @Emp_id, @Emp_address, @Emp_phone, @Emp_date, @Emp_salary)", connection);


                    DateTime joinDate;
                    if (DateTime.TryParse(Emp_date.Text, out joinDate))
                    {
                        cmd.Parameters.AddWithValue("@Emp_Name", Emp_Name.Text);
                        cmd.Parameters.AddWithValue("@Emp_id", Emp_id.Text);
                        cmd.Parameters.AddWithValue("@Emp_address", Emp_address.Text);
                        cmd.Parameters.AddWithValue("@Emp_phone", Emp_phone.Text);
                        cmd.Parameters.AddWithValue("@Emp_date", Emp_date.Text);
                        cmd.Parameters.AddWithValue("@Emp_salary", (Emp_salary.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Records Inserted Successfully");


                        Emp_Name.Text = "";
                        Emp_id.Text = "";
                        Emp_address.Text = "";
                        Emp_phone.Text = "";
                        Emp_date.Text = "";
                        Emp_salary.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid date.");
                    }

                    connection.Close();
                    LoadData();

                }
                else
                {
                    MessageBox.Show("Please fill in all fields.");
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

        private void clear_Click(object sender, EventArgs e)
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


        private void LoadData()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee2", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataGrid.DataSource = dt;
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


        private void update_Click(object sender, EventArgs e)
        {

            {
                try
                {
                    if (!string.IsNullOrEmpty(Emp_id.Text)) 
                    {
                        connection.Open();

                
                        string query = "UPDATE Employee2 SET ";
                        List<string> fieldsToUpdate = new List<string>();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;

                      
                        if (!string.IsNullOrEmpty(Emp_Name.Text))
                        {
                            fieldsToUpdate.Add("Emp_Name = @Emp_Name");
                            cmd.Parameters.AddWithValue("@Emp_Name", Emp_Name.Text);
                        }

                        if (!string.IsNullOrEmpty(Emp_address.Text))
                        {
                            fieldsToUpdate.Add("Emp_address = @Emp_address");
                            cmd.Parameters.AddWithValue("@Emp_address", Emp_address.Text);
                        }

                        if (!string.IsNullOrEmpty(Emp_phone.Text))
                        {
                            fieldsToUpdate.Add("Emp_phone = @Emp_phone");
                            cmd.Parameters.AddWithValue("@Emp_phone", Emp_phone.Text);
                        }

                        if (!string.IsNullOrEmpty(Emp_date.Text))
                        {
                            DateTime joinDate;
                            if (DateTime.TryParse(Emp_date.Text, out joinDate))
                            {
                                fieldsToUpdate.Add("Emp_date = @Emp_date");
                                cmd.Parameters.AddWithValue("@Emp_date", joinDate);
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid date.");
                                connection.Close();
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(Emp_salary.Text))
                        {
                            fieldsToUpdate.Add("Emp_salary = @Emp_salary");
                            cmd.Parameters.AddWithValue("@Emp_salary", Emp_salary.Text);
                        }

                      
                        if (fieldsToUpdate.Count > 0)
                        {
                            query += string.Join(", ", fieldsToUpdate);
                            query += " WHERE Emp_id = @Emp_id";
                            cmd.CommandText = query;
                            cmd.Parameters.AddWithValue("@Emp_id", Emp_id.Text);

                        
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record Updated Successfully");

                       
                            Emp_Name.Text = "";
                            Emp_id.Text = "";
                            Emp_address.Text = "";
                            Emp_phone.Text = "";
                            Emp_date.Text = "";
                            Emp_salary.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("No fields to update.");
                        }

                        connection.Close();
                        LoadData(); 
                    }
                    else
                    {
                        MessageBox.Show("Please enter the Employee ID.");
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

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Emp_id.Text))
               

                    {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM Employee2 WHERE Emp_id = @Emp_id", connection)
                    {
                        
                    };
                    cmd.Parameters.AddWithValue("@Emp_id", Emp_id.Text);
                   

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided Employee ID.");
                    }

                 
                    Emp_id.Text = "";
                

                    connection.Close();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Please enter the Employee ID to delete.");
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




