using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PasteleriaReposteria
{
    class BaseDeDatos
    {
        private string cadenaConexion = @"Data Source=mssql8.gear.host;Initial Catalog=Reposteria;User ID=reposteria;Password=Kd7VpdKSy6~!";

        public static string rol = "";


        public Boolean InicioSesion(string user, string psswd)
        {
            using (SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                string query = "select * from Credencial, Usuarios where NomUser= @nomuser and PsswdUser collate Latin1_General_CS_AS = @psswd " +
                    "and Credencial.idUsuario = Usuarios.idUsuario";
                string nombre = "";
                using (SqlCommand cmd = new SqlCommand(query, conx))
                {
                    conx.Open();
                    cmd.Parameters.AddWithValue("@nomuser", user);
                    cmd.Parameters.AddWithValue("@psswd", psswd);

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        nombre = lector.GetString(6);
                        rol = lector.GetInt32(4).ToString();
                    }
                    lector.Close();

                    if (String.IsNullOrEmpty(nombre))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        public Boolean RegistrarUsuario(string nombre, string direccion, string correo, string telefono, string user, string psswd)
        {
            using (SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                string query = "declare @identidad int INSERT INTO Usuarios(NombreUsuario, Direccion, Correo, Telefono) values(@nombre, @direccion, @correo, @telefono)" +
                    " set @identidad = SCOPE_IDENTITY() insert into Credencial(NomUser, PsswdUser, idUsuario) values(@user, @psswd, @identidad)";

                using (SqlCommand cmd = new SqlCommand(query, conx))
                {
                    conx.Open();
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@psswd", psswd);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public Boolean RegistrarCliente(string nombre, string direccion, string telefono, string correo)
        {
            try
            {
                using (SqlConnection conx = new SqlConnection(cadenaConexion))
                {
                    string query = "INSERT INTO Cliente(Nombre, Direccion, Telefono, Correo) values(@nombre, @direccion, @telefono, @correo)";

                    using (SqlCommand cmd = new SqlCommand(query, conx))
                    {
                        conx.Open();
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@correo", correo);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean RegistrarProducto(string nombre, double costo)
        {
            try
            {
                using(SqlConnection conx = new SqlConnection(cadenaConexion))
                {
                    string query = "INSERT INTO [dbo].[Productos] ([NombreProducto],[Costo]) VALUES(@nombre, @costo)";

                    using(SqlCommand cmd = new SqlCommand(query, conx))
                    {
                        conx.Open();
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@costo", costo);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Boolean RegistrarCotizacion(string detalles, double costo, int idCliente, string productos)
        {
            try
            {
                using(SqlConnection conx = new SqlConnection(cadenaConexion))
                {
                    string query = "INSERT INTO [dbo].[Cotizaciones]([Detalles],[CostoEstimado],[idCliente],[Productos]) VALUES(@detalles, @costo, @idCliente, @productos)";

                    using(SqlCommand cmd = new SqlCommand(query, conx))
                    {
                        conx.Open();
                        cmd.Parameters.AddWithValue("@detalles", detalles);
                        cmd.Parameters.AddWithValue("@costo", costo);
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Parameters.AddWithValue("@productos", productos);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean RegistrarVenta(string detalles, double total, string fecha, int idCliente, int idCot, string productos, string folio)
        {
            try
            {
                using(SqlConnection conx = new SqlConnection(cadenaConexion))
                {
                    using(SqlCommand cmd = new SqlCommand("exec sp_venta @Detalles, @Total, @FechaDeEntrega, @idCliente, @Productos, @idCot, @folio", conx))
                    {
                        conx.Open();
                        cmd.Parameters.AddWithValue("@Detalles", detalles);
                        cmd.Parameters.AddWithValue("@Total", total);
                        cmd.Parameters.AddWithValue("@FechaDeEntrega", fecha);
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Parameters.AddWithValue("@Productos", productos);
                        cmd.Parameters.AddWithValue("@idCot", idCot);
                        cmd.Parameters.AddWithValue("@folio", folio);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean RegistrarEntrega(int idEntrega, int estado)
        {
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                string query = "update Entregas set Estado = @estado where idEntrega = @idEntrega";
                using (SqlCommand cmd = new SqlCommand(query, conx))
                {
                    conx.Open();
                    cmd.Parameters.AddWithValue("@idEntrega", idEntrega);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public Boolean RegistrarPaquete(string nombre, string contenido, double total)
        {
            try
            {
                using (SqlConnection conx = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("insert into Paquetes(T0.NombrePaquete, Contenido, Total) values(@nombre, @contenido, @total)", conx))
                    {
                        conx.Open();
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@contenido", contenido);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean UpdateProducto(string nombre, string costo, string id)
        {
            try
            {
                using(SqlConnection conx = new SqlConnection(cadenaConexion))
                {
                    #region Query
                    string query = "UPDATE [dbo].[Productos] SET [NombreProducto] =  @nombreproducto,[Costo] = @costo  WHERE  idProducto = @id";
                    #endregion
                    using (SqlCommand cmd = new SqlCommand(query, conx))
                    {
                        conx.Open();
                        cmd.Parameters.AddWithValue("@nombreproducto", nombre);
                        cmd.Parameters.AddWithValue("@costo", costo);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public string[] GetLastCotizacion()
        {
            
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                #region Query y Variables
                string[] datos = new string[6];
                string idCot = ".", idCliente = ".", nombre = ".", productos = ".", detalles = ".", costo = ".";
                string query = @"SELECT TOP 1 T0.idCotizacion, T0.idCliente, T1.Nombre, T0.Productos, T0.Detalles, T0.CostoEstimado FROM Cotizaciones T0
                    left join Cliente T1 on T0.idCliente = T1.idCliente
                    ORDER BY idCotizacion DESC"; 
                #endregion
                using (SqlCommand cmd = new SqlCommand(query, conx))
                {
                    conx.Open();
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        idCot = Convert.ToString(lector.GetInt32(0));
                        idCliente = Convert.ToString(lector.GetInt32(1));
                        nombre = (lector.GetString(2));
                        productos = (lector.GetString(3));
                        detalles = (lector.GetString(4));
                        costo = Convert.ToString(lector.GetDouble(5));
                    }
                    lector.Close();
                    return datos = new string[] { idCot, idCliente, nombre, productos, detalles, costo };
                    
                }
            }
        }

        public string[] GetLastVenta()
        {
            using (SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                #region Query y Variables
                string[] datos = new string[6];
                string idPed = ".", idCliente = ".", nombre = ".", productos = ".", detalles = ".", costo = ".", fecha = "";
                string query = @"Select top 1 T0.idPedido, T0.idCliente, T1.Nombre, CONVERT(Date, T0.FechaDeEntrega,23),   T0.Detalles, T0.Productos, T0.Total from Pedido T0
                    left join Cliente T1 on T0.idCliente = T1.idCliente
                    order by idPedido DESC"; 
                #endregion
                using (SqlCommand cmd = new SqlCommand(query, conx))
                {
                    conx.Open();
                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        idPed = Convert.ToString(lector.GetInt32(0));
                        idCliente = Convert.ToString(lector.GetInt32(1));
                        nombre = (lector.GetString(2));
                        productos = (lector.GetString(5));
                        detalles = (lector.GetString(4));
                        costo = Convert.ToString(lector.GetDouble(6));
                        fecha = lector.GetDateTime(3).ToString("yyyy-MM-dd");
                    }
                    lector.Close();
                    return datos = new string[] { idPed, idCliente, nombre, fecha ,detalles, costo, productos };

                }
            }
        }

        public DataSet CheckDisponibilidad(string fecha)
        {
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT * FROM Pedido WHERE FechaDeEntrega = @fecha";
                using(SqlDataAdapter da = new SqlDataAdapter(query, conx))
                {
                    conx.Open();
                    da.SelectCommand.Parameters.AddWithValue("@fecha", fecha);

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conx.Close();

                    return ds;
                }
            }
        }

        public DataTable GetEntregas(int estado)
        {
            #region Query
            string query = @"Select T0.idEntrega, T2.idCliente, T2.Nombre, T2.Correo, T1.Detalles, T1.Total, T1.FechaDeEntrega, T1.idPedido, T3.nomEstado from Entregas T0
                left join Pedido T1 on T0.idPedido = T1.idPedido
                left join Cliente T2 on T0.idCliente = T2.idCliente
				left join Estados T3 on T0.Estado = T3.idEstado
                where T0.Estado = @estado";
            #endregion
            using (SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                using(SqlDataAdapter da = new SqlDataAdapter(query, conx))
                {
                    da.SelectCommand.Parameters.AddWithValue("@estado", estado);
                    conx.Open();
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    conx.Close();

                    return ds;
                }
            }
        }

        public DataTable GetEntregasDef()
        {
            #region Query
            string query = @"Select T0.idEntrega, T2.idCliente, T2.Nombre, T2.Correo, T1.Detalles, T1.Total, T1.FechaDeEntrega, T1.idPedido, T3.nomEstado from Entregas T0
                left join Pedido T1 on T0.idPedido = T1.idPedido
                left join Cliente T2 on T0.idCliente = T2.idCliente
				left join Estados T3 on T0.Estado = T3.idEstado
				order by T3.nomEstado desc"; 
            #endregion
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                using(SqlDataAdapter da = new SqlDataAdapter(query, conx))
                {
                    conx.Open();
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    return ds;
                }
            } 
        }

        public DataTable GetVentas()
        {
            #region Query
            string query = @"Select T0.idEntrega, T2.idCliente, T1.idPedido, T2.Nombre, T1.Detalles, T1.Total, T1.FechaDeEntrega, CONVERT(Date, T4.fecha,101) 'Fecha de Pago', T4.Folio 'Folio Banco', T3.nomEstado, T2.Correo from Entregas T0
                left join Pedido T1 on T0.idPedido = T1.idPedido
                left join Cliente T2 on T0.idCliente = T2.idCliente
				left join Estados T3 on T0.Estado = T3.idEstado
				left join Remisiones T4  on T0.idPedido = T4.idPedido
				order by T0.idEntrega asc"; 
            #endregion
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                using(SqlDataAdapter da = new SqlDataAdapter(query, conx))
                {
                    conx.Open();
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    return ds;
                }
            }
        }

        public DataSet GetCliente()
        {
            using (SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                conx.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT idCliente, Nombre FROM Cliente", conx);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conx.Close();

                return ds;
            }
        }

        public DataSet GetPaquete()
        {
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                conx.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT idPaquete, NombrePaquete from Paquetes", conx);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conx.Close();

                return ds;
            }
        }

        public DataSet GetPaqueteInfo(int paquete)
        {
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("exec sp_GetProductosFromPaquetes @paquete", conx))
                {
                    conx.Open();
                    da.SelectCommand.Parameters.AddWithValue("@paquete", paquete);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conx.Close();
                    return ds;
                }
            }
        }

        public DataTable GetPaqueteInfo1(int paquete)
        {
            using (SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("exec sp_PaqueteInfoAPI @paquete", conx))
                {
                    conx.Open();
                    da.SelectCommand.Parameters.AddWithValue("@paquete", paquete);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    conx.Close();
                    return ds;
                }
            }
        }

        public DataSet GetProductos()
        {
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                using(SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos", conx))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conx.Close();
                    return ds;
                }
            }
        }

        public DataTable GetProductoInfo1(int id)
        {
            using (SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Select * FROM Productos WHERE idProducto = @id", conx))
                {
                    da.SelectCommand.Parameters.AddWithValue("@id", id);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        public DataSet GetProductoInfo(int id)
        {
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                using(SqlDataAdapter da = new SqlDataAdapter("Select * FROM Productos WHERE idProducto = @id", conx))
                {
                    da.SelectCommand.Parameters.AddWithValue("@id", id);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        public DataSet GetCotizacion()
        {
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {

                #region Query
                string query = "Select T0.idCotizacion, T1.Nombre from Cotizaciones T0 " +
                            "left join Cliente T1 on T0.idCliente = T1.idCliente"; 
                #endregion
                using (SqlDataAdapter da = new SqlDataAdapter(query, conx))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        public DataSet GetCotizacionInfo(int id)
        {
            using(SqlConnection conx = new SqlConnection(cadenaConexion))
            {
                #region Query 
                string query = @"Select T0.idCotizacion, T0.idCliente, T1.Nombre, T0.Productos, T0.Detalles, T0.CostoEstimado from Cotizaciones T0 
left join Cliente T1 on T0.idCliente = T1.idCliente 
where T0.idCotizacion = @id"; 
                #endregion

                using (SqlDataAdapter da = new SqlDataAdapter(query, conx))
                {
                    da.SelectCommand.Parameters.AddWithValue("@id", id);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }
    }
}
