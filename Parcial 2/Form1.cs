using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Parcial_2
{
    public partial class Form1 : Form
    {

        private string connectionString = "Server=localhost,1433;Database=ConversorDB;User Id=sa;Password=TuPassword123;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            CargarHistorial();
        }

        private void btnConvertirF_Click(object sender, EventArgs e)
        {
            ConvertirDesdeFahrenheit();
        }

        private void btnConvertirC_Click(object sender, EventArgs e)
        {
            ConvertirDesdeCelsius();
        }

        private void btnConvertirK_Click(object sender, EventArgs e)
        {
            ConvertirDesdeKelvin();
        }

        private void ConvertirDesdeFahrenheit()
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtFahrenheit.Text))
                {
                    MessageBox.Show("Por favor, ingrese un valor en Fahrenheit.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }


                double fahrenheit = double.Parse(txtFahrenheit.Text);


                double celsius = (fahrenheit - 32) * 5 / 9;


                double kelvin = celsius + 273.15;


                lblFahrenheitResult.Text = fahrenheit.ToString("F2");
                lblCelsiusResult.Text = celsius.ToString("F2");
                lblKelvinResult.Text = kelvin.ToString("F2");


                GuardarConversion("Fahrenheit", fahrenheit, celsius, kelvin);


                CargarHistorial();

                MessageBox.Show("Conversión realizada y guardada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un número válido.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la conversión: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConvertirDesdeCelsius()
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtCelsius.Text))
                {
                    MessageBox.Show("Por favor, ingrese un valor en Celsius.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }


                double celsius = double.Parse(txtCelsius.Text);


                double fahrenheit = (celsius * 9 / 5) + 32;


                double kelvin = celsius + 273.15;


                lblFahrenheitResult.Text = fahrenheit.ToString("F2");
                lblCelsiusResult.Text = celsius.ToString("F2");
                lblKelvinResult.Text = kelvin.ToString("F2");


                GuardarConversion("Celsius", fahrenheit, celsius, kelvin);


                CargarHistorial();

                MessageBox.Show("Conversión realizada y guardada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un número válido.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la conversión: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConvertirDesdeKelvin()
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtKelvin.Text))
                {
                    MessageBox.Show("Por favor, ingrese un valor en Kelvin.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }


                double kelvin = double.Parse(txtKelvin.Text);


                double celsius = kelvin - 273.15;


                double fahrenheit = (celsius * 9 / 5) + 32;


                lblFahrenheitResult.Text = fahrenheit.ToString("F2");
                lblCelsiusResult.Text = celsius.ToString("F2");
                lblKelvinResult.Text = kelvin.ToString("F2");


                GuardarConversion("Kelvin", fahrenheit, celsius, kelvin);


                CargarHistorial();

                MessageBox.Show("Conversión realizada y guardada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un número válido.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la conversión: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // VERSIÓN MÍNIMA (pero funcional)
        private void GuardarConversion(string tipo, double f, double c, double k)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                new SqlCommand($"INSERT INTO Conversiones VALUES ('{tipo}',{f},{c},{k},GETDATE())", conn)
                    .ExecuteNonQuery();
            }
        }

        private void CargarHistorial()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var adapter = new SqlDataAdapter("SELECT * FROM Conversiones", conn);
                var dt = new DataTable();
                adapter.Fill(dt);
                dgvHistorial.DataSource = dt;
            }
        }
    }
}