using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto1
{
    public class DB
    {
        private string connectionString = @"Server=PEPEGA\SQLEXPRESS;Database=Productos;Trusted_Connection=True;Encrypt=false;";

        public bool GuardarCalculo(double valor1, double valor2, string operacion, double resultado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO Calculos (Valor1, Valor2, Operacion, Resultado, Fecha) 
                                   VALUES (@Valor1, @Valor2, @Operacion, @Resultado, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Valor1", valor1);
                    cmd.Parameters.AddWithValue("@Valor2", valor2);
                    cmd.Parameters.AddWithValue("@Operacion", operacion);
                    cmd.Parameters.AddWithValue("@Resultado", resultado);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en base de datos: " + ex.Message +
                              "\n\nVerifica que:\n" +
                              "1. Docker esté ejecutándose\n" +
                              "2. El contenedor de SQL Server esté corriendo\n" +
                              "3. El password sea correcto en DatabaseHelper.cs",
                              "Error de Base de Datos",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return false;
            }
        }

        public System.Data.DataTable ObtenerHistorial()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT Id, Valor1, Valor2, Operacion, Resultado, 
                                   FORMAT(Fecha, 'dd/MM/yyyy HH:mm:ss') as Fecha 
                                   FROM Calculos 
                                   ORDER BY Id DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener historial: " + ex.Message,
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }

            return dt;
        }
    }
}