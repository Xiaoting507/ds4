using System;
using System.Windows.Forms;

namespace Laboratorio122
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Botón Promedio - Calcula el promedio de las 3 notas
        private void btnPromedio_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener las notas de los textbox
                double nota1 = Convert.ToDouble(txtNota1.Text);
                double nota2 = Convert.ToDouble(txtNota2.Text);
                double nota3 = Convert.ToDouble(txtNota3.Text);

                // Validar que las notas estén en el rango válido (0-100 o 0-10 según tu sistema)
                if (nota1 < 0 || nota2 < 0 || nota3 < 0)
                {
                    MessageBox.Show("Las notas no pueden ser negativas.",
                        "Error de validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Calcular el promedio
                double promedio = (nota1 + nota2 + nota3) / 3;

                // Mostrar el resultado en el textbox de promedio
                txtPromedio.Text = promedio.ToString("F2");
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos en todas las notas.",
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
            txtNota1.Clear();
            txtNota2.Clear();
            txtNota3.Clear();
            txtPromedio.Clear();
            txtNota1.Focus(); // Pone el foco en el primer textbox
        }

        // Botón Salir - Cierra la aplicación
        private void btnSalir_Click(object sender, EventArgs e)
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