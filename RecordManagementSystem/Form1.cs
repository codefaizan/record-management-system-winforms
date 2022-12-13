using System;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;
using System.Reflection.Metadata.Ecma335;

namespace RecordManagementSystem
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (IsValid())
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();

                string conString = AppSettings.ConnectionString();
                MySqlConnection con = new MySqlConnection(conString);
                con.Open();

                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO employees(employee_name, employee_department, Image) VALUES(@name, @dept, @img)";
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@dept", textBox2.Text);
                cmd.Parameters.AddWithValue("@img", img);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record Added Successfully", "Success");
            }

        }

        private bool IsValid()
        {
            if (textBox1.Text.Trim() == string.Empty || textBox2.Text.Trim() == string.Empty)
            {
                MessageBox.Show("All fields are Required", "Required Field Error");
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BrowseImage();
        }

        private void BrowseImage()
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose Image(*.jpg; *.png, *.jpeg)|*.jpg; *.png; *.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            }

            //DialogResult result = openFileDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    try
            //    {
            //        this.pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            //        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //        pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 dataWindow = new Form2();
            dataWindow.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            if (Form2.emp_id != 0)
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();

                string conString = AppSettings.ConnectionString();
                MySqlConnection con = new MySqlConnection(conString);
                con.Open();

                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE employees set employee_name=@name, employee_department=@dept, Image=@img WHERE emp_id=@id";
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@dept", textBox2.Text);
                cmd.Parameters.AddWithValue("@img", img);
                cmd.Parameters.AddWithValue("@id", Form2.emp_id);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record Updated Successfully!", "Success");

                f2.Show();
                f2.LoadDataIntoDataGridView();
                this.Hide();
               
                
                
            }
            else
            {
                MessageBox.Show("No Record Selected!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            if (Form2.emp_id != 0)
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();

                string conString = AppSettings.ConnectionString();
                MySqlConnection con = new MySqlConnection(conString);
                con.Open();

                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM employees WHERE emp_id=@id";
                cmd.Parameters.AddWithValue("@id", Form2.emp_id);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record Deleted Successfully!", "Success");

                f2.Show();
                f2.LoadDataIntoDataGridView();
                this.Hide();



            }
            else
            {
                MessageBox.Show("No Record Selected!");
            }
        }
    }
}