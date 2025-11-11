using System;
using System.Web.UI;

namespace Laboratorio153
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string texto = TextBox1.Text;

            // Por seguridad, escapamos comillas simples para no romper el JavaScript.
            texto = texto.Replace("'", "\\'");

            Page.ClientScript.RegisterClientScriptBlock(
                typeof(Page),
                "MessageBox",
                "window.alert('Hola: " + texto + "');",
                true
            );
        }
    }
}
