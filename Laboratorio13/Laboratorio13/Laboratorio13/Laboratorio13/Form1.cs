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

namespace Laboratorio13
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            string connectionString = "Server=localhost,1433;Database=Northwind;User Id=sa;Password=TuPassword123;TrustServerCertificate=True;";


            SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();
            MessageBox.Show("Se abrió la conexión con el servidor SQL Server y se seleccionó la base de datos");

            string query = "SELECT ProductName FROM [dbo].[Products]";

            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataReader lector = comando.ExecuteReader();

            listBox1.Items.Clear();

            while (lector.Read())
            {
                listBox1.Items.Add(lector["ProductName"].ToString());
            }

            lector.Close();

            conexion.Close();
            MessageBox.Show("Se cerró la conexión.");

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
