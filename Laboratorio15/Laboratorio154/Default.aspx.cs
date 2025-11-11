using System;
using System.Web.UI;

namespace Laboratorio154
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSumar_Click(object sender, EventArgs e)
        {
            double n1, n2;

            if (double.TryParse(txtNumero1.Text, out n1) &&
                double.TryParse(txtNumero2.Text, out n2))
            {
                double suma = n1 + n2;
                lblResultado.Text = "La suma es: " + suma;
            }
            else
            {
                lblResultado.Text = "Ingrese números válidos en ambos campos.";
            }
        }
    }
}
