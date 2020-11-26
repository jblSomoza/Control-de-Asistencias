using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using SistemaAsistencias.Logica;

namespace SistemaAsistencias.Datos
{
    class UsuarioController
    {

        public bool insertarUsuario(UsuarioModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Usuario", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", "0");
                cmd.Parameters.AddWithValue("@nombres", parametros.nombres);
                cmd.Parameters.AddWithValue("@apellidos", parametros.apellidos);
                cmd.Parameters.AddWithValue("@usuario", parametros.usuario);
                cmd.Parameters.AddWithValue("@clave", parametros.clave);
                cmd.Parameters.AddWithValue("@icono", parametros.icono);
                cmd.Parameters.AddWithValue("@estado", "N");
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally 
            {
                ConexionMaestraController.cerrar();
            }
        }

        public bool editarUsuarios(UsuarioModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Usuario", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", parametros.idUsuario);
                cmd.Parameters.AddWithValue("@nombres", parametros.nombres);
                cmd.Parameters.AddWithValue("@apellidos", parametros.apellidos);
                cmd.Parameters.AddWithValue("@usuario", parametros.usuario);
                cmd.Parameters.AddWithValue("@clave", parametros.clave);
                cmd.Parameters.AddWithValue("@icono", parametros.icono);
                cmd.Parameters.AddWithValue("@estado", "M");
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestraController.cerrar();
            }
        }

        public bool eliminarUsuario(UsuarioModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Usuario", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", parametros.idUsuario);
                cmd.Parameters.AddWithValue("@nombres", "");
                cmd.Parameters.AddWithValue("@apellidos", "");
                cmd.Parameters.AddWithValue("@usuario", parametros.usuario);
                cmd.Parameters.AddWithValue("@clave", "");
                cmd.Parameters.AddWithValue("@icono", "");
                cmd.Parameters.AddWithValue("@estado", "E");
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public bool restaurarUsuario(UsuarioModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_restaurarUsuario", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", parametros.idUsuario);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public void buscarUsuario(ref DataTable dt, string buscador) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter da = new SqlDataAdapter("proc_buscarUsuario", ConexionMaestraController.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                ConexionMaestraController.cerrar();
            }
        }

        public void mostrarUsuarios(ref DataTable dt) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Usuarios", ConexionMaestraController.conectar);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public void obtenerIdUsuario(ref int idUsuario, string usuario) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_ObtenerIdUsuario", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", usuario);
                idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public void verificarUsuarios(ref string indicador) 
        {
            try
            {
                int idUser;
                ConexionMaestraController.abrir();
                SqlCommand da = new SqlCommand("SELECT idUsuario FROM Usuarios", ConexionMaestraController.conectar);
                idUser = Convert.ToInt32(da.ExecuteScalar());
                ConexionMaestraController.cerrar();
                indicador = "Correcto";
            }
            catch (Exception)
            {
                indicador = "Incorrecto";
            }
        }

        public void validarUsuario(UsuarioModel parametros, ref int id) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_validarUsuario", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clave", parametros.clave);
                cmd.Parameters.AddWithValue("@usuario", parametros.usuario);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                id = 0;
            }
            finally { ConexionMaestraController.cerrar(); }
        }

    }
}
