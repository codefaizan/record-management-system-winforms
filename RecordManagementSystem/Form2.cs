using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordManagementSystem
{
    public partial class Form2 : Form
    {
        public static int emp_id;
        public Form2()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
           
        }

        public void LoadDataIntoDataGridView()
        {
            string conString = AppSettings.ConnectionString();
            MySqlConnection con = new MySqlConnection(conString);
            con.Open();

            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from employees";

            MySqlDataReader sdr = cmd.ExecuteReader();
            DataTable records = new DataTable();
            records.Load(sdr);
            
            dataGridView1.DataSource= records;

            
            ProcessImage();
        }

        private void ProcessImage()
        {
            //DataGridViewImageColumn img = new DataGridViewImageColumn();
            //img.ImageLayout = DataGridViewImageCellLayout.Stretch;
            ((DataGridViewImageColumn)dataGridView1.Columns[dataGridView1.Columns.Count - 1]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.RowTemplate.Height = 130;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            emp_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            Form1 f1 = new Form1();

            f1.Show();

            f1.textBox1.Text = (dataGridView1.SelectedRows[0].Cells[1].Value).ToString();
            f1.textBox2.Text = (dataGridView1.SelectedRows[0].Cells[2].Value).ToString();

            GetImageFromDataGridView(f1);

            this.Close();
            
        }

        private void GetImageFromDataGridView(Form1 f1)
        {
            byte[] imgData = (byte[])dataGridView1.SelectedRows[0].Cells[3].Value;
            MemoryStream ms = new MemoryStream(imgData);
            f1.pictureBox1.Image = Image.FromStream(ms);
            f1.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }
    }
}
