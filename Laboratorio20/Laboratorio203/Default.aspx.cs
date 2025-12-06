using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorio203
{
    public partial class Default : Page
    {
        // Cadena de conexión desde Web.config
        private string connectionString = "Server=sqlserver,1433;Database=productos;User Id=sa;Password=TuPassword123;TrustServerCertificate=True;";
            
        // Flag para saber si es nuevo registro o actualización (igual que el Lab14)
        private bool EsNuevo
        {
            get { return ViewState["EsNuevo"] != null && (bool)ViewState["EsNuevo"]; }
            set { ViewState["EsNuevo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Estado inicial (igual que frmProductos_Load del Lab14)
                EstadoInicial();
                CargarLaptops();
            }
        }

        // Estado inicial del formulario (replica el Load del Lab14)
        private void EstadoInicial()
        {
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btnBuscar.Enabled = true;

            txtId.Enabled = false;
            txtNombre.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
            txtBuscarId.Enabled = true;

            LimpiarCampos();
            pnlMensaje.Visible = false;
        }

        // Botón NUEVO (replica tsbNuevo_Click del Lab14)
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnEliminar.Enabled = false;
            btnBuscar.Enabled = false;

            txtId.Enabled = false;
            txtNombre.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            txtBuscarId.Enabled = false;

            LimpiarCampos();
            txtNombre.Focus();
            EsNuevo = true;
            pnlMensaje.Visible = false;
        }

        // Botón GUARDAR (replica tsbGuardar_Click del Lab14)
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string procedimiento = EsNuevo ? "SP_InsertarLaptop" : "SP_ActualizarLaptop";

                    using (SqlCommand cmd = new SqlCommand(procedimiento, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Si es actualización, agregar el ID
                        if (!EsNuevo)
                        {
                            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                        }

                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                        cmd.Parameters.AddWithValue("@stock", Convert.ToDouble(txtStock.Text));

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        string mensaje = EsNuevo ?
                            "Registro ingresado correctamente!" :
                            "Registro actualizado correctamente!";

                        MostrarMensaje(mensaje, true);
                    }
                }

                // Volver al estado inicial después de guardar
                EstadoInicial();
                CargarLaptops();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        // Botón CANCELAR (replica tsbCancelar_Click del Lab14)
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        // Botón ELIMINAR (replica tsbEliminar_Click del Lab14)
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MostrarMensaje("No hay registro seleccionado para eliminar", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EliminarLaptop", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MostrarMensaje("Registro eliminado correctamente!", true);
                    }
                }

                EstadoInicial();
                CargarLaptops();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        // Botón BUSCAR (replica tsbBuscar_Click del Lab14)
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscarId.Text))
            {
                MostrarMensaje("Ingrese un ID para buscar", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_BuscarLaptopPorID", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtBuscarId.Text));

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Cambiar estado del formulario para edición
                            btnNuevo.Enabled = false;
                            btnGuardar.Enabled = true;
                            btnCancelar.Enabled = true;
                            btnEliminar.Enabled = true;
                            btnBuscar.Enabled = false;

                            txtId.Enabled = false;
                            txtNombre.Enabled = true;
                            txtPrecio.Enabled = true;
                            txtStock.Enabled = true;
                            txtBuscarId.Enabled = false;

                            // Cargar datos
                            txtId.Text = reader["id"].ToString();
                            txtNombre.Text = reader["nombre"].ToString();
                            txtPrecio.Text = reader["precio"].ToString();
                            txtStock.Text = reader["stock"].ToString();

                            txtNombre.Focus();
                            EsNuevo = false;
                            pnlMensaje.Visible = false;
                        }
                        else
                        {
                            MostrarMensaje("Ningun registro encontrado con el Id ingresado!", false);
                        }

                        reader.Close();
                    }
                }

                txtBuscarId.Text = "";
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        // Método para cargar todos los laptops en el GridView
        private void CargarLaptops()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ListarLaptops", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvLaptops.DataSource = dt;
                        gvLaptops.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar laptops: " + ex.Message, false);
            }
        }

        // Método para limpiar campos
        private void LimpiarCampos()
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtBuscarId.Text = string.Empty;
        }

        // Método para mostrar mensajes
        private void MostrarMensaje(string mensaje, bool esExito)
        {
            pnlMensaje.CssClass = esExito ? "message message-success" : "message message-error";
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }
    }
}
