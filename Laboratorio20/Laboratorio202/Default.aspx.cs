using System;
using System.Text;
using System.Web.UI;

namespace Laboratorio202
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

                // Obtener la dimensión ingresada
                int n = Convert.ToInt32(txtDimension.Text);

                // Generar la matriz
                StringBuilder matrizHtml = new StringBuilder();
                matrizHtml.Append("<table class='matriz-table'>");

                // Crear la matriz N x N
                for (int i = 0; i < n; i++)
                {
                    matrizHtml.Append("<tr>");

                    for (int j = 0; j < n; j++)
                    {
                        // La diagonal inversa es cuando i + j = n - 1
                        // Por ejemplo, en una matriz 5x5:
                        // (0,4), (1,3), (2,2), (3,1), (4,0) son la diagonal inversa
                        bool esDiagonalInversa = (i + j == n - 1);

                        int valor = esDiagonalInversa ? 1 : 0;
                        string cssClass = esDiagonalInversa ? "diagonal" : "";

                        matrizHtml.AppendFormat(
                            "<td class='{0}'>{1}</td>",
                            cssClass,
                            valor
                        );
                    }

                    matrizHtml.Append("</tr>");
                }

                matrizHtml.Append("</table>");

                // Mostrar resultados
                lblDimension.Text = n.ToString();
                lblDimension2.Text = n.ToString();
                litMatriz.Text = matrizHtml.ToString();
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
