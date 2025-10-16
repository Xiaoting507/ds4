using System;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Laboratorio12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Evento del Button1 - Calcular distancia
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de velocidad y tiempo desde los textbox
                double velocidad = Convert.ToDouble(textBox1.Text);
                double tiempo = Convert.ToDouble(textBox2.Text);

                // Calcular la distancia recorrida (distancia = velocidad * tiempo)
                double distancia = velocidad * tiempo;

                // Mostrar el resultado en label4
                label4.Text = "Distancia recorrida: " + distancia.ToString("F2") + " unidades";
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos.",
                    "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del Button2 - Limpiar campos
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            label4.Text = "";
        }

        // Evento del Button3 - Salir de la aplicación
        private void button3_Click(object sender, EventArgs e)
        {
            // Preguntar al usuario si realmente desea salir
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}