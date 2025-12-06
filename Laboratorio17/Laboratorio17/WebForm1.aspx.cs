using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Laboratorio17
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConnectionStringSettings connString = ConfigurationManager.ConnectionStrings["ConexionNorthwind"];
            SqlConnection conexion = new SqlConnection(connString.ConnectionString);

            using (SqlCommand cmd = new SqlCommand("SalesByCategory", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = "Seafood";
                conexion.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            GridV.DataSource = reader;
                            GridV.DataBind();
                        }
                    }
                }
            }
        }
    }
}