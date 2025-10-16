using System;
using System.Windows.Forms;
using static System.Windows.Forms.MonthCalendar;

namespace Laboratorio123
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Botón Semiperimetro - Calcula el semiperímetro del triángulo
        private void btnSemiperimetro_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener las longitudes de los lados
                double ladoA = Convert.ToDouble(txtLadoA.Text);
                double ladoB = Convert.ToDouble(txtLadoB.Text);
                double ladoC = Convert.ToDouble(txtLadoC.Text);

                // Validar que los lados sean positivos
                if (ladoA <= 0 || ladoB <= 0 || ladoC <= 0)
                {
                    MessageBox.Show("Los lados del triángulo deben ser valores positivos.",
                        "Error de validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Validar que los ladosformen un triángulo válido
                if (ladoA + ladoB <= ladoC || ladoA + ladoC <= ladoB || ladoB + ladoC <= ladoA)
                {
                    MessageBox.Show("Los valores ingresados no forman un triángulo válido.\nLa suma de dos lados debe ser mayor que el tercer lado.",
                        "Triángulo inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Calcular el semiperímetro: s = (a + b + c) / 2
                double semiperimetro = (ladoA + ladoB + ladoC) / 2;

                // Mostrar el resultado
                txtSemiperimetro.Text = semiperimetro.ToString("F2");
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos en todos los lados.",
                    "Error de entrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Botón Área - Calcula el área del triángulo usando la fórmula de Herón
        private void btnArea_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener las longitudes de los lados
                double ladoA = Convert.ToDouble(txtLadoA.Text);
                double ladoB = Convert.ToDouble(txtLadoB.Text);
                double ladoC = Convert.ToDouble(txtLadoC.Text);

                // Validar que los lados sean positivos
                if (ladoA <= 0 || ladoB <= 0 || ladoC <= 0)
                {
                    MessageBox.Show("Los lados del triángulo deben ser valores positivos.",
                        "Error de validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Validar que los ladosformen un triángulo válido
                if (ladoA + ladoB <= ladoC || ladoA + ladoC <= ladoB || ladoB + ladoC <= ladoA)
                {
                    MessageBox.Show("Los valores ingresados no forman un triángulo válido.\nLa suma de dos lados debe ser mayor que el tercer lado.",
                        "Triángulo inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Calcular el semiperímetro
                double s = (ladoA + ladoB + ladoC) / 2;

                // Calcular el área usando la fórmula de Herón: A = √[s(s-a)(s-b)(s-c)]
                double area = Math.Sqrt(s * (s - ladoA) * (s - ladoB) * (s - ladoC));

                // Mostrar solo el resultado del área
                txtArea.Text = area.ToString("F2");
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos en todos los lados.",
                    "Error de entrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Botón Reset - Limpia todos los campos
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtLadoA.Clear();
            txtLadoB.Clear();
            txtLadoC.Clear();
            txtSemiperimetro.Clear();
            txtArea.Clear();
            txtLadoA.Focus();
        }

        // Botón Salida - Cierra la aplicación
        private void btnSalida_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea salir de la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}