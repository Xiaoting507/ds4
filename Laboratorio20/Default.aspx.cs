using System;
using System.Text;
using System.Web.UI;

namespace Laboratorio201
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No es necesario código aquí
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que la página sea válida
                if (!Page.IsValid)
                {
                    return;
                }

                // Obtener el número ingresado
                int numero = Convert.ToInt32(txtNumero.Text);

                // Generar la tabla de multiplicar hasta 25
                StringBuilder tablaHtml = new StringBuilder();

                for (int i = 1; i <= 25; i++)
                {
                    int resultado = numero * i;
                    tablaHtml.AppendFormat(
                        "<div class='fila-tabla'>{0} × {1} = <strong>{2}</strong></div>",
                        numero, i, resultado
                    );
                }

                // Mostrar resultados
                lblNumero.Text = numero.ToString();
                litTabla.Text = tablaHtml.ToString();
                pnlResultado.Visible = true;
            }
            catch (Exception ex)
            {
                // En caso de error, mostrar mensaje
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}
