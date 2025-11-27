using Microsoft.VisualBasic.Logging;
using System.CodeDom;
using System.Linq.Expressions;

namespace Proyecto1
{

    public partial class Form1 : Form
    {
        Operaciones op = new Operaciones();
        DB db = new DB();
        double carry = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn0.Text);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn1.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn2.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn3.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn4.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn5.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn6.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn7.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn8.Text);
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.btn_Selec(txbResultado.Text, btn9.Text);
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {
            if (txbResultado.Text.Length == 0)
            {
                txbResultado.Text = carry.ToString();
                carry = 0;
                txbCarry.Text = carry.ToString();
                txbCarry.Hide();
            }
            else
            {
                double val, resultado;
                switch (txbCarry.Text.Last())
                {
                    case '+':
                        val = ParsearDecimal(txbResultado.Text);
                        resultado = op.Sumar(val, carry);

                        db.GuardarCalculo(carry, val, "+", resultado);

                        carry = resultado;
                        txbCarry.Text = String.Format($"{carry.ToString()} {btnSumar.Text}");
                        txbResultado.Text = "";
                        break;

                    case '-':
                        val = ParsearDecimal(txbResultado.Text);
                        resultado = op.Restar(val, carry);

                        db.GuardarCalculo(carry, val, "-", resultado);

                        carry = resultado;
                        txbCarry.Text = String.Format($"{carry.ToString()} {btnRestar.Text}");
                        txbResultado.Text = "";
                        break;

                    case 'x':
                        val = ParsearDecimal(txbResultado.Text);
                        resultado = op.Producto(val, carry);

                        db.GuardarCalculo(carry, val, "x", resultado);

                        carry = resultado;
                        txbCarry.Text = String.Format($"{carry.ToString()} {btnProd.Text}");
                        txbResultado.Text = "";
                        break;

                    case '/':
                        val = ParsearDecimal(txbResultado.Text);

                        if (val == 0)
                        {
                            MessageBox.Show("No se puede dividir entre cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        resultado = op.Dividir(val, carry);

                        db.GuardarCalculo(carry, val, "/", resultado);

                        carry = resultado;
                        txbCarry.Text = String.Format($"{carry.ToString()} {btnDividir.Text}");
                        txbResultado.Text = "";
                        break;

                    case '^':
                        val = ParsearDecimal(txbResultado.Text);
                        resultado = op.Potencia(val, carry);


                        db.GuardarCalculo(carry, val, "^", resultado);

                        carry = resultado;
                        txbCarry.Text = String.Format($"{carry.ToString()} {btnPotencia.Text}");
                        txbResultado.Text = "";
                        break;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            txbResultado.Text = op.Eliminar(txbResultado.Text);
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            txbCarry.Show();
            if (carry == 0)
            {
                carry = ParsearDecimal(txbResultado.Text);
                txbCarry.Text = String.Format($"{carry.ToString()} {btnSumar.Text}");
                txbResultado.Text = "";
            }
            else
            {
                double resultado, val;
                if (txbResultado.Text.Length == 0)
                {

                }
                else
                {
                    val = ParsearDecimal(txbResultado.Text);
                    resultado = op.Sumar(val, carry);
                    carry = resultado;
                    txbCarry.Text = String.Format($"{carry.ToString()} {btnSumar.Text}");
                    txbResultado.Text = "";
                }
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            txbCarry.Show();
            if (carry == 0)
            {
                carry = ParsearDecimal(txbResultado.Text);
                txbCarry.Text = String.Format($"{carry.ToString()} {btnRestar.Text}");
                txbResultado.Text = "";
            }
            else
            {
                double resultado, val;
                if (txbResultado.Text.Length == 0)
                {

                }
                else
                {
                    val = ParsearDecimal(txbResultado.Text);
                    resultado = op.Restar(val, carry);
                    carry = resultado;
                    txbCarry.Text = String.Format($"{carry.ToString()} {btnRestar.Text}");
                    txbResultado.Text = "";
                }
            }
        }

        private void btnProd_Click(object sender, EventArgs e)
        {
            txbCarry.Show();
            if (carry == 0)
            {
                carry = ParsearDecimal(txbResultado.Text);
                txbCarry.Text = String.Format($"{carry.ToString()} {btnProd.Text}");
                txbResultado.Text = "";
            }
            else
            {
                double resultado, val;
                if (txbResultado.Text.Length == 0)
                {

                }
                else
                {
                    val = ParsearDecimal(txbResultado.Text);
                    resultado = op.Producto(val, carry);
                    carry = resultado;
                    txbCarry.Text = String.Format($"{carry.ToString()} {btnProd.Text}");
                    txbResultado.Text = "";
                }
            }
        }
        private void btnDividir_Click(object sender, EventArgs e)
        {
            txbCarry.Show();
            if (carry == 0)
            {
                carry = ParsearDecimal(txbResultado.Text);
                txbCarry.Text = String.Format($"{carry.ToString()} {btnDividir.Text}");
                txbResultado.Text = "";
            }
            else
            {
                double resultado, val;
                if (txbResultado.Text.Length == 0)
                {

                }
                else
                {
                    val = ParsearDecimal(txbResultado.Text);
                    resultado = op.Dividir(val, carry);
                    carry = resultado;
                    txbCarry.Text = String.Format($"{carry.ToString()} {btnDividir.Text}");
                    txbResultado.Text = "";
                }
            }
        }


        private void btnSigno_Click(object sender, EventArgs e)
        {
            double val;
            val = ParsearDecimal(txbResultado.Text);

            if (val > 0)
            {
                val = val * -1;
            }
            else if (val < 0)
            {
                val = Math.Abs(val);
            }

            txbResultado.Text = val.ToString();
        }

        private void btnClearE_Click(object sender, EventArgs e)
        {
            txbResultado.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbResultado.Text = "";
            txbCarry.Text = "";
            txbCarry.Hide();
            carry = 0;
        }

        private void btnPotencia_Click_1(object sender, EventArgs e)
        {
            txbCarry.Show();
            if (carry == 0)
            {
                carry = ParsearDecimal(txbResultado.Text);
                txbCarry.Text = String.Format($"{carry.ToString()} {btnPotencia.Text}");
                txbResultado.Text = "";
            }
            else
            {
                double resultado, val;
                if (txbResultado.Text.Length == 0)
                {

                }
                else
                {
                    val = ParsearDecimal(txbResultado.Text);
                    resultado = op.Potencia(val, carry);
                    carry = resultado;
                    txbCarry.Text = String.Format($"{carry.ToString()} {btnPotencia.Text}");
                    txbResultado.Text = "";
                }
            }
        }

        private void btnRaiz_Click(object sender, EventArgs e)
        {

            if (txbResultado.Text.Length > 0 && txbResultado.Text != "0")
            {
                double val = ParsearDecimal(txbResultado.Text);


                if (val < 0)
                {
                    MessageBox.Show("No se puede calcular la raíz cuadrada de un número negativo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double resultado = op.Raices(val);


                db.GuardarCalculo(val, 0, "√", resultado);

                txbResultado.Text = resultado.ToString();
            }
        }

        private void btnDecimal_Click_1(object sender, EventArgs e)
        {

            if (txbResultado.Text.Contains(".") || txbResultado.Text.Contains(","))
            {
                return;
            }


            if (string.IsNullOrEmpty(txbResultado.Text) || txbResultado.Text == "0")
            {
                txbResultado.Text = "0.";
            }
            else
            {

                txbResultado.Text += ".";
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                var historial = db.ObtenerHistorial();

                if (historial == null || historial.Rows.Count == 0)
                {
                    MessageBox.Show("No hay cálculos guardados.",
                                  "Historial Vacío",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;
                }

                Form formHistorial = new Form();
                formHistorial.Text = $"Historial - {historial.Rows.Count} cálculos";
                formHistorial.Size = new Size(800, 500);
                formHistorial.StartPosition = FormStartPosition.CenterScreen;

                DataGridView dgv = new DataGridView();
                dgv.DataSource = historial;
                dgv.Dock = DockStyle.Fill;
                dgv.ReadOnly = true;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AllowUserToAddRows = false;

                formHistorial.Controls.Add(dgv);
                formHistorial.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPI_Click(object sender, EventArgs e)
        {
            txbResultado.Text = Math.PI.ToString("F10", System.Globalization.CultureInfo.InvariantCulture);
        }


        private double ParsearDecimal(string texto)
        {
            try
            {

                texto = texto.Trim();


                texto = texto.Replace(',', '.');


                return double.Parse(texto, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }
    }


    public class Operaciones()
    {
        public double Calcular(string val) 
        {
            return Convert.ToDouble(val);
        }

        public string btn_Selec(string val, string num)
        {
            if (val == "0")
            {
                return num;
            }
            else
                return val + num;
        }

        public string Eliminar(string val)
        {
            if (val == "0")
            {
                return "0";
            }
            else if (val.Length == 0)
            {
                return "0";
            }
            else
                return val.Substring(0, val.Length - 1);
        }

        public double Sumar(double val, double carry)
        {
            return val + carry;
        }

        public double Restar(double val, double carry) 
        {
            return carry - val;
        }

        public double Producto(double val, double carry) 
        {
            return val * carry;
        }

        public double Dividir(double val, double carry) 
        {
            return carry / val;
        }

        public double Potencia(double val, double carry) 
        {
            return Math.Pow(carry, val);
        }

        public double Raices(double val) 
        {
            return Math.Sqrt(val);
        }
    }
}